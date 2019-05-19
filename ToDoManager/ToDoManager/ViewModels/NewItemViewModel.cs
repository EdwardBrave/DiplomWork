using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using ToDoManager.Models;
using ToDoManager.Services;
using ToDoManager.Views;

namespace ToDoManager.ViewModels
{

    class EventCategoryArgs: EventArgs
    {
        public readonly IEnumerable<Category> categories;
        public EventCategoryArgs(IEnumerable<Category> categories)
        {
            this.categories = categories;
        }
    }

    class NewItemViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public ObservableCollection<Category> Categories{ get; set; }
        public Category SelectedCategory { get; set; }

        //public event EventHandler<EventCategoryArgs> addCategory;

        public NewItemViewModel()
        {
            Date = DateTime.Now;
            Time = new TimeSpan(Date.Hour, Date.Minute, Date.Second);
            Item = new Item();
            LoadCategories();
        }

        private async void LoadCategories()
        {
            Categories = new ObservableCollection<Category>();
            if (DataStore is DataBaseStore dataBaseStore)
            {
                var items = await dataBaseStore.GetCateoriesAsync();
                foreach (var item in items)
                {
                    var category = new Category() {
                        Id = item.Id,
                        Name = item.Name,
                        Section = item.Section + ": " + item.Name,
                        Icon = item.Icon
                    };
                    Categories.Add(category);
                }
            }
            Console.WriteLine();
        }

        public async void SaveItem()
        {
            Item.Category = SelectedCategory.Name;
            await DataStore.AddItemAsync(Item);
            MessagingCenter.Send(this, "AddItem", Item);
        }
    }
}
