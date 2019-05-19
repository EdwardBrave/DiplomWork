using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Xamarin.Forms;

namespace ToDoManager.Services
{
    public static class Locale
    {
        private static string lang = "en";
        private static string embeddedFilePath = $"ToDoManager.Localization.langPack-{lang}.xml";

        private static XmlDocument langData;

        public static XmlDocument Data
        {
            get
            {
                if (langData != null)
                {
                    return langData;
                }
                langData = new XmlDocument();
                var assembly = Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream(embeddedFilePath);
                langData.Load(stream); 
                return langData;
            }
        }

        public static string Tr(string code)
        {
            XmlNode node = Data.SelectSingleNode("/Locale/" + code);
            return node?.InnerText ?? "Empty";
        }
    }
}
