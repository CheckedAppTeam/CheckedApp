using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckedAppProject.LOGIC.Services.Logger
{
    public class Logger : IAppLogger
    {
        private readonly string timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        private readonly string logFilePath;
        public Logger(string fileName)
        {
            logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }
        public void LogToFile(string message)
        {
                try
                {
                    // Append the log message to the file
                    File.AppendAllText(logFilePath, $"[{timestamp}] - {message}\n");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[{timestamp}] -Error logging to file: {ex.Message}");
                }
        }
        public void LogToConsole(string message) 
        {
            var timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            Console.WriteLine($"[{timestamp}] - Log: {{message}}");
        }

        public void LogException(Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Exception: {exception}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void LogToFileAndConsole(string message) 
        {
            LogToConsole(message);
            LogToFile(message);
        }
    }
}
