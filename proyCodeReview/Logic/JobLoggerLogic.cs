using System;
using System.Configuration;

namespace proyCodeReview.Logic
{
    public class JobLoggerLogic
    {
        public enum LogType
        {
            Message,
            Warning,
            Error
        }

        private readonly bool _logToFile;
        private readonly bool _logToConsole;
        private readonly bool _logMessage;
        private readonly bool _logWarning;
        private readonly bool _logError;
        private readonly bool _logToDatabase;

        public JobLoggerLogic()
        {
            _logError = bool.Parse(ConfigurationManager.AppSettings["Config_logError"]);
            _logMessage = bool.Parse(ConfigurationManager.AppSettings["Config_logMessage"]);
            _logWarning = bool.Parse(ConfigurationManager.AppSettings["Config_logWarning"]);
            _logToDatabase = bool.Parse(ConfigurationManager.AppSettings["Config_logToDatabase"]);
            _logToFile = bool.Parse(ConfigurationManager.AppSettings["Config_logToFile"]);
            _logToConsole = bool.Parse(ConfigurationManager.AppSettings["Config_logToConsole"]);
        }

        public string LogMessage(string pSMessage, LogType pLogType)
        {
            try
            {
                if (string.IsNullOrEmpty(pSMessage)) return "-1";

                pSMessage = pSMessage.Trim();

                int logType;
                string dirTypeLog;

                switch (pLogType)
                {
                    case LogType.Message:
                        if (_logMessage)
                        {
                            logType = 1;
                            dirTypeLog = ConfigurationManager.AppSettings["LogFileDirectoryTypeMessage"];
                            return WriteLog(pSMessage, logType, dirTypeLog, pLogType);
                        }
                        break;
                    case LogType.Error:
                        if (_logError)
                        {
                            logType = 2;
                            dirTypeLog = ConfigurationManager.AppSettings["LogFileDirectoryTypeError"];
                            return WriteLog(pSMessage, logType, dirTypeLog, pLogType);
                        }
                        break;
                    case LogType.Warning:
                        if (_logWarning)
                        {
                            logType = 3;
                            dirTypeLog = ConfigurationManager.AppSettings["LogFileDirectoryTypeWarning"];
                            return WriteLog(pSMessage, logType, dirTypeLog, pLogType);
                        }
                        break;
                }
                return "-1";
            }
            catch (Exception)
            {
                return "-1";
            }
        }

        private string WriteLog(string pSMessage, int logType, string dirTypeLog, LogType pLogType)
        {
            var logId = Guid.NewGuid();
            var dateFormatLog = DateTime.UtcNow.ToString("yyyyMMdd HH:mm:ss");

            pSMessage = dateFormatLog + " - " + logId + " - " + pSMessage + " - " + pLogType;

            if (_logToDatabase)
            {
                WriteLogToDataBase(pSMessage, logType);
            }

            if (_logToFile)
            {
                WriteLogToFile(pSMessage, dirTypeLog);
            }

            if (_logToConsole)
            {
                WriteLogToConsole(pSMessage, pLogType);
            }

            return logId.ToString();
        }

        private void WriteLogToConsole(string pSMessage, LogType pLogType)
        {
            switch (pLogType)
            {
                case LogType.Message:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            Console.WriteLine(pSMessage);
        }

        private void WriteLogToFile(string pSMessage, string dirTypeLog)
        {
            var dateFormatFileName = DateTime.UtcNow.ToString("yyyyMMdd");
            string dataMessage = null;
            var fileName = dirTypeLog + "LogFile_" + dateFormatFileName + ".txt";
            if (!System.IO.Directory.Exists(dirTypeLog))
            {
                System.IO.Directory.CreateDirectory(dirTypeLog);
            }
            if (System.IO.File.Exists(fileName))
            {
                dataMessage = System.IO.File.ReadAllText(fileName);
            }
            dataMessage = dataMessage + pSMessage + '\n';
            System.IO.File.WriteAllText(fileName, dataMessage);
        }

        private void WriteLogToDataBase(string pSMessage, int logType)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                connection.Open();
                var messageLogDateBase = pSMessage;
                var command = new System.Data.SqlClient.SqlCommand("Insert into Log (column1, column2) Values('" + messageLogDateBase + "', " + logType + ")");
                command.ExecuteNonQuery();
            }
        }
    }
}