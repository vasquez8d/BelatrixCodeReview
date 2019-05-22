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

        public string LogMessage(string pSMessage, LogType pELogType)
        {
            try
            {
                if (string.IsNullOrEmpty(pSMessage)) return "-1";

                pSMessage = pSMessage.Trim();

                int logType;
                string dirTypeLog;

                switch (pELogType)
                {
                    case LogType.Message:
                        if (_logMessage)
                        {
                            logType = 1;
                            dirTypeLog = ConfigurationManager.AppSettings["LogFileDirectoryTypeMessage"];
                            return WriteLog(pSMessage, logType, dirTypeLog, pELogType);
                        }
                        break;
                    case LogType.Error:
                        if (_logError)
                        {
                            logType = 2;
                            dirTypeLog = ConfigurationManager.AppSettings["LogFileDirectoryTypeError"];
                            return WriteLog(pSMessage, logType, dirTypeLog, pELogType);
                        }
                        break;
                    case LogType.Warning:
                        if (_logWarning)
                        {
                            logType = 3;
                            dirTypeLog = ConfigurationManager.AppSettings["LogFileDirectoryTypeWarning"];
                            return WriteLog(pSMessage, logType, dirTypeLog, pELogType);
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

        private string WriteLog(string pSMessage, int pILogType, string pSDirTypeLog, LogType pELogType)
        {
            var logId = Guid.NewGuid();
            var dateFormatLog = DateTime.UtcNow.ToString("yyyyMMdd HH:mm:ss");

            pSMessage = dateFormatLog + " - " + logId + " - " + pELogType + " - " + pSMessage;

            if (_logToDatabase)
            {
                WriteLogToDataBase(pSMessage, pILogType);
            }

            if (_logToFile)
            {
                WriteLogToFile(pSMessage, pSDirTypeLog);
            }

            if (_logToConsole)
            {
                WriteLogToConsole(pSMessage, pELogType);
            }

            return logId.ToString();
        }

        private void WriteLogToConsole(string pSMessage, LogType pELogType)
        {
            switch (pELogType)
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

        private void WriteLogToFile(string pSMessage, string oSDirTypeLog)
        {
            var dateFormatFileName = DateTime.UtcNow.ToString("yyyyMMdd");
            string dataMessage = null;
            var fileName = oSDirTypeLog + "LogFile_" + dateFormatFileName + ".txt";
            if (!System.IO.Directory.Exists(oSDirTypeLog))
            {
                System.IO.Directory.CreateDirectory(oSDirTypeLog);
            }
            if (System.IO.File.Exists(fileName))
            {
                dataMessage = System.IO.File.ReadAllText(fileName);
            }
            dataMessage = dataMessage + pSMessage + '\n';
            System.IO.File.WriteAllText(fileName, dataMessage);
        }

        private void WriteLogToDataBase(string pSMessage, int pILogType)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                connection.Open();
                var messageLogDateBase = pSMessage;
                var command = new System.Data.SqlClient.SqlCommand("Insert into Log (column1, column2) Values('" + messageLogDateBase + "', " + pILogType + ")");
                command.ExecuteNonQuery();
            }
        }
    }
}