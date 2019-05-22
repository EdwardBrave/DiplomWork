using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoManager.Models;
using ToDoManager.ViewModels;

namespace ToDoManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            MessagingCenter.Subscribe<NewItemViewModel, Item>(this, "AddItem", RefreshPage);
            MessagingCenter.Subscribe<MenuViewModel>(this, "LangRefresh", Refresh);
        }

        private void Refresh(object sender)
        {
            var binding = BindingContext;
            BindingContext = null;
            BindingContext = binding;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Label = "Item 1"
                
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
            MessagingCenter.Subscribe<NewItemViewModel, Item>(this, "AddItem", RefreshPage);
        }

        private void RefreshPage(NewItemViewModel obj, Item newItem)
        {
            newItem.Priority = (newItem.Importance + newItem.Urgency) / 2F;
            viewModel.Item = newItem;
            BindingContext = null;
            BindingContext = viewModel;
        }

        private async void EditClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage(new NewItemViewModel(viewModel.Item))));
        }
    }
}