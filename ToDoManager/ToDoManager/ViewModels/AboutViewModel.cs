using System;
using System.Windows.Input;

using Xamarin.Forms;
using ToDoManager.Services;

namespace ToDoManager.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = Locale.Tr("lc_005");

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://sites.google.com/view/edward-braves-portfolio")));
        }

        public string lc_002 => Locale.Tr("lc_002");
        public string lc_003 => Locale.Tr("lc_003");
        public string lc_004 => Locale.Tr("lc_004");
        public string lc_005 => Locale.Tr("lc_005");

        public ICommand OpenWebCommand { get; }
    }
}