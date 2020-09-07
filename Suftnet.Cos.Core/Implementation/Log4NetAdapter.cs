namespace Suftnet.Cos.Core
{
    using System;
    using System.Configuration;
    using log4net;
    using log4net.Config;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    public class LogAdapter : ILogger
    {
        private readonly ILog _log;
        private readonly ILogViewer _logViewer;      
        public LogAdapter(ILogViewer logViewer)
        {
            XmlConfigurator.Configure();

            _log = LogManager.GetLogger(ConfigurationManager.AppSettings["LoggerName"]);
            _logViewer = logViewer;
        }

        public void Log(string message, EventLogSeverity logSeverity)
        {
            switch (logSeverity)
            {
                case EventLogSeverity.Debug:
                    _log.Debug(message);
                    break;
                case EventLogSeverity.Error:
                    _log.Error(message);
                    break;
                case EventLogSeverity.Fatal:
                    _log.Error(message);
                    break;
                case EventLogSeverity.Information:
                    _log.Info(message);
                    break;
                case EventLogSeverity.None:
                    _log.Info(message);
                    break;
                case EventLogSeverity.Warning:
                    _log.Warn(message);
                    break;
            }

            this.Logger(message);
        }

        public void LogError(Exception ex)
        {
             this.Logger(ex.Message);
            _log.Error(ex.Message);
        }

        private void Logger(string message)
        {
            _logViewer.Insert(new LogDto { CreatedBy = Environment.UserName, CreatedDt = DateTime.UtcNow, Description = message });
        }
    }
    /// <summary>
    /// EventLogSeverity
    /// </summary>
 
}
