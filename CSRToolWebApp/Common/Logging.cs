using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CSRToolWebApp.Common
{
    public static class Logging
    {
        public static void LogError(string message, string sessionId = null)
        {
            // Get calling method name
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            string callingMethod = methodBase.ReflectedType.FullName + "." + methodBase.Name;

            // Get calling assembly name
            string callingAssembly = Assembly.GetCallingAssembly().ManifestModule.Name;

            Log(message, LogLevel.Error, callingAssembly, callingMethod, sessionId);
        }

        public static void LogWarning(string message, string sessionId = null)
        {
            // Get calling method name
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            string callingMethod = methodBase.ReflectedType.FullName + "." + methodBase.Name;

            // Get calling assembly name
            string callingAssembly = Assembly.GetCallingAssembly().ManifestModule.Name;

            Log(message, LogLevel.Warn, callingAssembly, callingMethod, sessionId);
        }

        public static void LogInfo(string message, string sessionId = null)
        {
            // Get calling method name
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            string callingMethod = methodBase.ReflectedType.FullName + "." + methodBase.Name;

            // Get calling assembly name
            string callingAssembly = Assembly.GetCallingAssembly().ManifestModule.Name;

            Log(message, LogLevel.Info, callingAssembly, callingMethod, sessionId);
        }

        private static void Log(string message, LogLevel level, string callingAssembly, string callingMethod, string sessionId = null)
        {
            try
            {
                LogEventInfo logEvent = new LogEventInfo(level, "", message);

                logEvent.Properties["CallingAssembly"] = callingAssembly;
                logEvent.Properties["CallingMethod"] = callingMethod;
                logEvent.Properties["SessionId"] = String.IsNullOrEmpty(sessionId) ? string.Empty : sessionId;

                Logger logger = LogManager.GetLogger("MyLogger");
                logger.Log(logEvent);

            }
            catch (Exception) { }
        }
    }
}