using System;
using UnityEngine;
using ILogger = Script.Libraries.Logger.LoggerBase.ILogger;

namespace Script.Libraries.Logger.Loggers
{
public class UnityMainLogger : ILogger
{
    public void Log(string log)
    {
        Debug.Log(log);
    }

    public void LogWarning(string warningLog)
    {
        Debug.LogWarning(warningLog);
    }

    public void LogError(string errorLog)
    {
        Debug.LogError(errorLog);
    }

    public void LogException(Exception exception)
    {
        Debug.LogException(exception);
    }
}
}