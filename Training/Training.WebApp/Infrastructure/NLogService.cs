using NLog;
using System;
using Training.BAL;

namespace Training.WebApp.Infrastructure
{
    public class NLogService : ILogService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void DebugWriteToLog(DateTime time, string recordInfo, string record)
        {
            Logger.Info("---");
            Logger.Info(time);
            Logger.Debug(recordInfo);
            Logger.Debug(record);
            Logger.Info("---");
        }

        public void ErrorWriteToLog(DateTime time, string recordInfo, string record)
        {
            Logger.Info("---");
            Logger.Info(time);
            Logger.Error(recordInfo);
            Logger.Error(record);
            Logger.Info("---");
        }

        public void FatalWriteToLog(DateTime time, string recordInfo, string record)
        {
            Logger.Info("---");
            Logger.Info(time);
            Logger.Fatal(recordInfo);
            Logger.Fatal(record);
            Logger.Info("---");
        }

        public void InfoWriteToLog(DateTime time, string recordInfo, string record)
        {
            Logger.Info("---");
            Logger.Info(time);
            Logger.Info(recordInfo);
            Logger.Info(record);
            Logger.Info("---");
        }

        public void WarnWriteToLog(DateTime time, string recordInfo, string record)
        {
            Logger.Info("---");
            Logger.Info(time);
            Logger.Warn(recordInfo);
            Logger.Warn(record);
            Logger.Info("---");
        }
    }
}