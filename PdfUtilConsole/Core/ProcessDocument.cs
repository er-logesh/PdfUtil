using AutoMapper;
using CommandLine;
using Common.PdfUtil.Core.Interface;
using Common.PdfUtil.Core.Model;
using Microsoft.Extensions.Configuration;
using PdfUtil.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using static Common.PdfUtil.Core.Interface.IDocOperations;
using static PdfUtil.Util.LogToConsole;

namespace PdfUtil.Core
{
    public class ProcessDocument
    {
        private IEnumerable<IDocOperations> docOperations;
        private ConfigurationRoot configuration;
        private IMapper mapper;

        public ProcessDocument(IEnumerable<IDocOperations> docOperations, IConfiguration configuration, IMapper mapper)
        {
            this.docOperations = docOperations;
            this.configuration = configuration as ConfigurationRoot;
            this.mapper = mapper;
        }
        public void Run()
        {
            GetCommandLineArgs();
        }
        private void GetCommandLineArgs()
        {
            var cmdArgs = Environment.GetCommandLineArgs();
            Parser.Default.ParseArguments<PdfUtil.Model.BaseOperation>(cmdArgs.Skip(1))
                .WithParsed(_parsed =>
                {
                    MatchArgsToObject(_parsed);
                })
                .WithNotParsed(_notParsed =>
                {

                });
        }
        private void MatchArgsToObject(Model.BaseOperation options)
        {
            var cmdArgs = Environment.GetCommandLineArgs();
            switch (options.Operation)
            {
                case PdfOperation.Unlock:
                    ParseUnlock();
                    break;
                case PdfOperation.Protect:
                    ParseProtect();
                    break;
                default:
                    break;
            }
            void ParseUnlock()
            {
                Parser.Default.ParseArguments<UnlockPdf>(cmdArgs.Skip(1))
                .WithParsed(_parsed =>
                {
                    var unlockPdf = docOperations.OfType<Common.PdfUtil.Core.Implementations.UnlockPdf>().FirstOrDefault();
                    unlockPdf.ProcessUpdateEvent -= ProcessUpdateEvent;
                    unlockPdf.ProcessUpdateEvent += ProcessUpdateEvent;
                    unlockPdf.ProcessDocument(mapper.Map<ProtectUnlockOperation>(_parsed));
                })
                .WithNotParsed(_notParsed =>
                {

                });
            }
            void ParseProtect()
            {
                Parser.Default.ParseArguments<UnlockPdf>(cmdArgs.Skip(1))
                .WithParsed(_parsed =>
                {
                    var protect = docOperations.OfType<Common.PdfUtil.Core.Implementations.ProtectPdf>().FirstOrDefault();
                    protect.ProcessUpdateEvent -= ProcessUpdateEvent;
                    protect.ProcessUpdateEvent += ProcessUpdateEvent;
                    protect.ProcessDocument(mapper.Map<ProtectUnlockOperation>(_parsed));
                })
                .WithNotParsed(_notParsed =>
                {

                });
            }
        }

        private void ProcessUpdateEvent(ProcessEventArgs eventArgs)
        {
            Log(eventArgs.Message, Enum.Parse<ConsoleMessageType>(eventArgs.Status.ToString()));
        }
    }
}
