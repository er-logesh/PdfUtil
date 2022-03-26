using System;

namespace PdfUtil.Util
{
    public static class LogToConsole
    {
        public enum ConsoleMessageType { Error, Warning, Success, Info }

        public static void Log(string message, ConsoleMessageType type = ConsoleMessageType.Info)
        {
            switch (type)
            {
                case ConsoleMessageType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case ConsoleMessageType.Warning:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case ConsoleMessageType.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case ConsoleMessageType.Info:
                    Console.ResetColor();
                    break;
            }
            Console.WriteLine(message);
            Console.ResetColor();
        }

    }
}
