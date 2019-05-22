using System;
using ToDoManager.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
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