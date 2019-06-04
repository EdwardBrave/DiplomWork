using System;

using ToDoManager.Datas;
using ToDoManager.Models;

namespace ToDoManager.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public string lc_009 => Locale.Tr("lc_009");
        public string lc_010 => Locale.Tr("lc_010");
        public string lc_011 => Locale.Tr("lc_011");
        public string lc_012 => Locale.Tr("lc_012");
        public string lc_013 => Locale.Tr("lc_013");
        public string lc_019 => Locale.Tr("lc_019");

        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Label;
            Item = item;
        }
    }
}
