using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using ToDoManager.Datas;
using ToDoManager.Models;
using ToDoManager.Views;

namespace ToDoManager.ViewModels
{

    public class NewItemViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public ObservableCollection<Category> Categories{ get; set; }
        public Category SelectedCategory { get; set; }

        private bool isEditMode;

        public string lc_009 => Locale.Tr("lc_009");
        public string lc_010 => Locale.Tr("lc_010");
        public string lc_012 => Locale.Tr("lc_012");
        public string lc_013 => Locale.Tr("lc_013");
        public string lc_014 => Locale.Tr("lc_014");
        public string lc_015 => Locale.Tr("lc_015");
        public string lc_017 => Locale.Tr("lc_017");
        public string lc_018 => Locale.Tr("lc_018");

        public NewItemViewModel(Item currentItem = null)
        {
            Date = DateTime.Now;
            Time = new TimeSpan(Date.Hour, Date.Minute, Date.Second);
            isEditMode = currentItem != null;
            Item = currentItem ?? new Item();
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
                    if (Item?.Category != null && Item.Category.IndexOf(category.Name) != -1)
                    {
                        SelectedCategory = category;
                        MessagingCenter.Send(this, "RefreshCategory", category);
                    }
                }
            }
            Console.WriteLine();
        }

        public async void SaveItem()
        {
            Item.Category = SelectedCategory.Name;
            if (isEditMode)
                await DataStore.UpdateItemAsync(Item);
            else
                await DataStore.AddItemAsync(Item);
            MessagingCenter.Send(this, "AddItem", Item);
        }
    }
}
