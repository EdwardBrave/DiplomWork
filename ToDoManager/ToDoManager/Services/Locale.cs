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
        private static string embeddedFilePath = "ToDoManager.Localization.langPack-#.xml";

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
                using (Stream stream = assembly.GetManifestResourceStream(embeddedFilePath.Replace("#", lang)))
                    langData.Load(stream); 
                return langData;
            }
        }

        public static string Lang
        {
            get => lang;

            set
            {
                lang = value;
                langData = null;
            }
        }

        public static string Tr(string code)
        {
            XmlNode node = Data.SelectSingleNode("/Locale/" + code);
            return node?.InnerText ?? "Empty";
        }
    }
}
