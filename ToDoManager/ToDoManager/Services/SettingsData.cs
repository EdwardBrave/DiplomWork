using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ToDoManager.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsData))]
namespace ToDoManager.Services
{

    class SettingsData : ISettings<string, string>
    {
        public const string Language = "lang";

        Dictionary<string, string> settings;

        string configPath = System.IO.Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
               "config.json");

        public SettingsData()
        {
            settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(configPath));
            if (settings == null)
            {
                settings = new Dictionary<string, string>();
            }
        }

        public void SetValue(string key, string value)
        {
            settings.Remove(key);
            settings.Add(key, value);
            RefreshData();
        }
        public string GetValue(string key)
        {
            return settings[key];
        }

        public void RemoveValue(string key)
        {
            settings.Remove(key);
            RefreshData();
        }

        private void RefreshData()
        {
            File.WriteAllText(configPath, JsonConvert.SerializeObject(settings));
        }
    }
}
