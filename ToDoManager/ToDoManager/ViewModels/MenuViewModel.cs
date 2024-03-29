﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

using ToDoManager.Models;
using Xamarin.Forms;

namespace ToDoManager.ViewModels
{

    class MenuViewModel
    {
        private ISettings<string,string> Settings => DependencyService.Get<ISettings<string, string>>() ?? new SettingsData();

        public string lc_005 => Locale.Tr("lc_005");
        public string lc_006 => Locale.Tr("lc_006");
        public string lc_007 => Locale.Tr("lc_007");
        public string lc_008 => Locale.Tr("lc_008");
        public string lc_020 => Locale.Tr("lc_020");
        public string lc_021 => Locale.Tr("lc_021");

        public MenuViewModel()
        {
            Locale.Lang = Settings.GetValue(SettingsData.Language) ?? "en";
        }

        public void RefreshLanguage(string index)
        {
            if(index != Locale.Lang)
            {
                Locale.Lang = index;
                Settings.SetValue(SettingsData.Language, index);
                MessagingCenter.Send(this, "LangRefresh");
            }
        }
    }
}
