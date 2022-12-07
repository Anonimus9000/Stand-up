using System;

namespace Script.Libraries.Logger.LoggerBase
{
public interface ILogger
{
    void Log(string log);
    void LogWarning(string warningLog);
    void LogError(string errorLog);
    void LogException(Exception exception);
}
}