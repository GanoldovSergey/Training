using System;
using System.Configuration;
using Training.DAL.Services.Interfaces;

namespace Training.WebApp.Infrastructure
{
    public class ConfigManager: IConfigManager
    {
        public string DatabaseId => AppSettings(Constants.DatabaseId);
        public string UserCollectionId => AppSettings(Constants.UserCollectionId);
        public string TestCollectionId => AppSettings(Constants.TestCollectionId);
        public string QuestionCollectionId => AppSettings(Constants.QuestionCollectionId);
        public string Endpoint => AppSettings(Constants.Endpoint);
        public string AuthKey => AppSettings(Constants.AuthKey);

        private string AppSettings(string keyName)
        {
            //Because of the tests
            var localConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\web.config";
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = localConfigurationFile };

            Configuration config =
                ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            var value = config.AppSettings.Settings[keyName];

            return value.Value;
        }
    }
}
