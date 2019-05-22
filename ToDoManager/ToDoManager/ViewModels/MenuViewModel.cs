using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

using ToDoManager.Services;
using Xamarin.Forms;

namespace ToDoManager.ViewModels
{
    public class Language
    {
        public string Index { get; set; }
        public string Name { get; set; }

        public Language(string index, string name)
        {
            this.Index = index;
            this.Name = name;
        }
    }



    class MenuViewModel
    {
        

        public string lc_005 => Locale.Tr("lc_005");
        public string lc_006 => Locale.Tr("lc_006");
        public string lc_007 => Locale.Tr("lc_007");
        public string lc_008 => Locale.Tr("lc_008");

        public MenuViewModel()
        {
            
        }

        public void RefreshLanguage(string index)
        {
            if(index != Locale.Lang)
            {
                Locale.Lang = index;
                MessagingCenter.Send(this, "LangRefresh");
            }
        }
    }
}
