using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ToDoManager.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsData))]
namespace ToDoManager.Models
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
            if (File.Exists(configPath))
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
            try
            {
                return settings[key];
            }
            catch (KeyNotFoundException ex)
            {
                return null;
            }
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
