namespace Suftnet.Cos.Core
{
    using System;
    using Suftnet.Cos.Common;
    public interface ILogger
    {
        void Log(string message, EventLogSeverity logSeverity);
        void LogError(Exception ex);
    }
}
