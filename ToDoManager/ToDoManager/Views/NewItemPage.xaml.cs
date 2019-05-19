using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoManager.ViewModels;

namespace ToDoManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        NewItemViewModel viewModel;

        public NewItemPage()
        {
            InitializeComponent();

            BindingContext = this.viewModel = new NewItemViewModel();
        }

        void ImportanceSliderChanged(object sender, ValueChangedEventArgs e)
        {
            ImportanceLabel.Text = Math.Round(e.NewValue).ToString();
        }

        void UrgencySliderChanged(object sender, ValueChangedEventArgs e)
        {
            UrgencyLabel.Text = Math.Round(e.NewValue).ToString();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            viewModel.SaveItem();
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}