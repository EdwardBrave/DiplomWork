using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

using ToDoManager.Datas;
using ToDoManager.Views;
using System.Collections.Generic;
using ToDoManager.Models;

namespace ToDoManager.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public string lc_007 => Locale.Tr("lc_007");
        public string lc_016 => Locale.Tr("lc_016");

        public ItemsViewModel()
        {
            Title = lc_007;
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemViewModel, Item>(this, "AddItem", async (obj, item) => await ExecuteLoadItemsCommand());
            MessagingCenter.Subscribe<ItemsPage, Item>(this, "ItemTaskFinished", RemoveFinishedItem);
        }

        async void RemoveFinishedItem(ItemsPage obj, Item item)
        {
            var archiveItem = new ArchiveItem(await DataStore.GetItemAsync(item.Id));
            await DataStore.DeleteItemAsync(item.Id);
            Items.Remove(item);
            archiveItem.Id = null;
            archiveItem.Date = DateTime.Now.ToString();
            await ArchiveStore.AddItemAsync(archiveItem);  
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
                var items = await DataStore.GetItemsAsync(true);
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