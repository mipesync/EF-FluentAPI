﻿namespace EF_FluentAPI.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void LogInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[info]: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{message}\n");
        }
    }
}
