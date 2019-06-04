using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoManager.ViewModels;
using ToDoManager.Datas;

namespace ToDoManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        NewItemViewModel viewModel;

        public NewItemPage(NewItemViewModel itemViewModel = null)
        {
            InitializeComponent();

            BindingContext = this.viewModel = itemViewModel ?? new NewItemViewModel();

            MessagingCenter.Subscribe<NewItemViewModel, Category>(this, "RefreshCategory", RefreshCategory);
            MessagingCenter.Subscribe<MenuViewModel>(this, "LangRefresh", Refresh);
        }

        private void Refresh(object sender)
        {
            var binding = BindingContext;
            BindingContext = null;
            BindingContext = binding;
        }

        void RefreshCategory(NewItemViewModel obj, Category category)
        {
            categoryPicker.SelectedItem = category;
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