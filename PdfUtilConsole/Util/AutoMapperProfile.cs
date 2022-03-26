using AutoMapper;
using Common.PdfUtil.Core.Model;
using PdfUtil.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfUtil.Util
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UnlockPdf, ProtectUnlockOperation>();
        }
    }
}
