namespace Suftnet.Cos.Core
{
    using System;
    using System.Configuration;
    using System.Text;
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

                    if (GeneralConfiguration.Configuration.ExecutingContext.Equals(ExecutingContext.TEST))
                    {
                        _log.Debug(message);
                        Logger(message);
                    }

                    break;
                case EventLogSeverity.Error:

                    _log.Error(message);
                    Logger(message);

                    break;
                case EventLogSeverity.Fatal:

                    _log.Error(message);
                    Logger(message);

                    break;
                case EventLogSeverity.Information:

                    if (GeneralConfiguration.Configuration.ExecutingContext.Equals(ExecutingContext.TEST))
                    {
                        _log.Info(message);
                        Logger(message);
                    }

                    break;
                case EventLogSeverity.None:

                    if (GeneralConfiguration.Configuration.ExecutingContext.Equals(ExecutingContext.TEST))
                    {
                        _log.Info(message);
                        Logger(message);
                    }
                    break;
                case EventLogSeverity.Warning:
                    if (GeneralConfiguration.Configuration.ExecutingContext.Equals(ExecutingContext.TEST))
                    {
                        _log.Warn(message);
                        Logger(message);
                    }
                    break;
            }

        }

        public void LogError(Exception ex)
        {
            var messages = Build(ex);

            this.Logger(messages);
            _log.Error(messages);
        }

        #region private function
        private string Build(System.Exception ex)
        {
            var content = new StringBuilder();

            if (ex.StackTrace != null)
            {
                content.Append("StackTrace");
                content.AppendLine();
                content.Append(ex.StackTrace);
            }

            if (!string.IsNullOrEmpty(ex.Message))
            {
                content.Append("Messages");
                content.AppendLine();
                content.Append(ex.Message);
            }

            content.AppendLine();
            content.Append("--------------------------------------------");
            content.AppendLine();
            if (ex.Data != null && ex.Data.Count > 0)
            {
                foreach (object item in ex.Data.Keys)
                {
                    if (item != null && ex.Data != null && ex.Data[item] != null)
                    {
                        content.AppendFormat("{0} = {1}", item, ex.Data[item]);
                    }
                    content.AppendLine();
                }
            }

            if (ex.InnerException != null)
            {
                content.Append("--------------------------------------------");
                content.AppendLine();
                content.Append("Inner Exception");
                content.AppendLine();
                content.Append("Messages");
                content.AppendLine();
                content.Append(ex.InnerException.Message);

                if (ex.InnerException.InnerException != null)
                {
                    content.Append("Messages");
                    content.AppendLine();
                    content.Append(ex.InnerException.InnerException.Message);
                }
            }
            return content.ToString();
        }
        private void Logger(string message)
        {
            _logViewer.Insert(new LogDto { CreatedBy = Environment.UserName, CreatedDt = DateTime.UtcNow, Description = message });
        }

        #endregion
    }

}
