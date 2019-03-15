using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace ToDoManager.Services
{
    public static class Lang
    {
        private static string _lang = "en";
    
        private static string _defLangDataPath = Path.GetFullPath("DefLangData.xml");

        private static XmlDocument _langData;

        public static XmlDocument Data() => _langData;

        public static void Data(string path)
        {
            if (path == null || path == "") return;
            _langData = new XmlDocument();
            _langData.Load(path);
        }

        public static string Tr(string code, string lang = null)
        {
            if (_langData == null)
            {
                _langData = new XmlDocument();
                _langData.Load(_defLangDataPath);
            }
            lang = lang ?? _lang;
            XmlNode node = _langData.SelectSingleNode("/Translations/" + code);
            return node?.SelectSingleNode(lang)?.InnerText ?? node?.SelectSingleNode("en")?.InnerText ?? "TranslationError";
        }
    }
}
