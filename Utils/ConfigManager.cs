using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace AlphaPoint_QA.Utils
{
    public static class ConfigManager
    {
        public const string APPSETTINGFILENAME = Const.ConfigFileName;

        static Config _Instance;
        public static AlphaPointWebDriver apwd; 

        public static Config Instance
        {
            get
            {

                if (_Instance == null)
                {
                    if (File.Exists(APPSETTINGFILENAME))
                    {
                        string jsontext = File.ReadAllText(APPSETTINGFILENAME);

                        _Instance = JsonConvert.DeserializeObject<Config>(jsontext);
                    }
                    else
                    {
                        _Instance = new Config();
                    }

                }
                return _Instance;
            }
        }

        public static object GetAppSetting(string key)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                return ConfigurationManager.AppSettings[key];
            return null;
        }

    }


}
