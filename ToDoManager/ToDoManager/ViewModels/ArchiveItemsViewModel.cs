using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

using ToDoManager.Models;
using ToDoManager.Views;
using System.Collections.Generic;
using ToDoManager.Services;

namespace ToDoManager.ViewModels
{
    public class ArchiveItemsViewModel : BaseViewModel
    {
        public ObservableCollection<ArchiveItem> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public string lc_021 => Locale.Tr("lc_021");

        public ArchiveItemsViewModel()
        {
            Title = lc_021;
            Items = new ObservableCollection<ArchiveItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemViewModel, ArchiveItem>(this, "AddItem", async (obj, item) => await ExecuteLoadItemsCommand());
            MessagingCenter.Subscribe<ArchiveItemsPage, ArchiveItem>(this, "AcrhiveTaskRemoved", RemoveArchiveItem);
        }

        async void RemoveArchiveItem(ArchiveItemsPage obj, ArchiveItem item)
        {
            await ArchiveStore.DeleteItemAsync(item.Id);
            Items.Remove(item);
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var categories = new List<Category>();
                if (DataStore is DataBaseStore dataBaseStore)
                {
                    categories = await dataBaseStore.GetCateoriesAsync() as List<Category>;
                }
                var items = await ArchiveStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    var category = categories.Where<Category>(obj => obj.Name == item.Category).FirstOrDefault();
                    if (category != null)
                    {
                        item.Category = category.Section + ": " + category.Name;
                    }
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}