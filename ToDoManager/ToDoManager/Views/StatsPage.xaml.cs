using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoManager.ViewModels;

namespace ToDoManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StatsPage : ContentPage
	{
		public StatsPage()
		{
			InitializeComponent();

            BindingContext = new StatsViewModel();
            MessagingCenter.Subscribe<MenuViewModel>(this, "LangRefresh", Refresh);
        }

        private void Refresh(object sender)
        {
            var binding = BindingContext;
            BindingContext = null;
            BindingContext = binding;
        }
    }
}