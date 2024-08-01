namespace API.Utilities
{
    public static class Logger
    {
        private static readonly string basePath = @"C:\Logs";

        public static void Log(string message, LogFileType fileType = LogFileType.Application, LogLevelType levelType = LogLevelType.Info)
        {
            try
            {
                string msg = $"{DateTime.Now:dd-MM-yyyy HH:mm:ss.fff} -- [{levelType}]: " +
                    message + Environment.NewLine;
                File.AppendAllText(GetLogTextFilePath(GetLogFilePath(fileType)), msg);
            }
            catch (DirectoryNotFoundException)
            {
                //var sadasd = GetLogFilePath(fileType);
                //using var text = File.CreateText(GetLogFilePath(fileType));
                Directory.CreateDirectory(GetLogFilePath(fileType));
                Log(message, fileType, levelType);
            }
            catch
            {
                //NoLog
            }
        }
        private static string GetLogFilePath(LogFileType fileType)
        {
            string fileName = fileType.ToString(); /*== LogFileType.Application ? "Application" : "Vendor";*/
            return Path.Combine(Path.Combine(basePath, fileName));
        }

        private static string GetLogTextFilePath(string filePath)
        {
            string logFile = $"Log_{DateTime.Now:yyyyMMdd}_{DateTime.Now.Hour}.txt";
            return Path.Combine(filePath, logFile);
        }
    }
    public enum LogFileType
    {
        Application=0,
        Person = 1,
        Izin = 2
    }
    public enum LogLevelType
    {
        Info = 1,
        Warning = 2,
        Error = 3,
    }
}

