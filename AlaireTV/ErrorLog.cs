using System;
using System.IO;

namespace AlaireTV
{
    public class ErrorLog
    {
        private string logFile = "errorlog.txt";

        public void LogError(string message)
        {
            File.AppendAllText(logFile, $"{DateTime.Now}: {message}\n");
        }

        public void LogEvent(string message)
        {
            File.AppendAllText(logFile, $"{DateTime.Now}: Event - {message}\n");
        }
    }
}
