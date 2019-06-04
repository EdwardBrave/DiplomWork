using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoManager.Datas;
using ToDoManager.ViewModels;

namespace ToDoManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArchiveItemsPage : ContentPage
    {
        ArchiveItemsViewModel viewModel;

        public ArchiveItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ArchiveItemsViewModel();
            MessagingCenter.Subscribe<MenuViewModel>(this, "LangRefresh", Refresh);
        }

        private void Refresh(object sender)
        {
            var binding = BindingContext;
            BindingContext = null;
            BindingContext = binding;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        void OnArchiveTaskRemoved(object sender, ToggledEventArgs e)
        {
            if (e.Value && sender is Switch switcher && switcher.BindingContext is ArchiveItem item)
            {
                MessagingCenter.Send(this, "AcrhiveTaskRemoved", item);
            }
                
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}