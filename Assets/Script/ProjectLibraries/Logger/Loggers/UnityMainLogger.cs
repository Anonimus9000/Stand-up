using System;
using UnityEngine;

namespace Script.ProjectLibraries.Logger.Loggers
{
public class UnityMainLogger : LoggerBase.ILogger
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