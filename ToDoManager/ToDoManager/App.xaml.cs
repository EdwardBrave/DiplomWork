using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ToDoManager.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ToDoManager
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
