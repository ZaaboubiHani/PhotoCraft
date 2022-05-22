using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoCraft.Functions
{
    public class ConfigFunctions
    {

        public ConfigFunctions()
        {

        }

        public async Task SetValue(string key, string value)
        {
            await Task.Run(() =>
            {
                try
                {
                    System.Configuration.ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                    fileMap.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\App.config";
                    Configuration configFile = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                    var settings = configFile.AppSettings.Settings;
                    if (settings[key] == null)
                    {
                        settings.Add(key, value);
                    }
                    else
                    {
                        settings[key].Value = value;
                    }
                    configFile.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                }
                catch (ConfigurationErrorsException)
                {
                    Console.WriteLine("Error writing app settings");
                }
            });
        }

        public async Task<string> GetValue(string key)
        {
            string result = string.Empty;
            await Task.Run(async () =>
            {
                try
                {
                    var appSettings = ConfigurationManager.AppSettings;
                    result = appSettings[key] ?? "Not Found";
                    if (result == "Not Found")
                    {
                        await SetValue("Theme", "Dark");
                    }
                    Console.WriteLine(result);
                }
                catch (ConfigurationErrorsException)
                {
                    Console.WriteLine("Error reading app settings");
                }
            });
            return result;
        }
    }
}
