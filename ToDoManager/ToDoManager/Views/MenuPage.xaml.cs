using ToDoManager.Models;
using System;
using System.Linq;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ToDoManager.ViewModels;
using System.ComponentModel;

namespace ToDoManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public List<Language> Languages { get; set; }

        private MenuViewModel viewModel;

        public MenuPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MenuViewModel();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Browse, Title=viewModel.lc_007 },
                new HomeMenuItem {Id = MenuItemType.About, Title=viewModel.lc_005 }
            };

            ListViewMenu.ItemsSource = menuItems;
            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };

            Languages = new List<Language>
            {
                new Language("en", "English"),
                new Language("de", "Deutsch"),
                new Language("ru", "Русский"),
                new Language("ua", "Українська")
            };

            language.ItemsSource = Languages;
            language.SelectedItem = Languages.Where(item => item.Index == Services.Locale.Lang).FirstOrDefault();
            language.SelectedIndexChanged += OnSelectLanguage;

            MessagingCenter.Subscribe<MenuViewModel>(this, "LangRefresh", Refresh);
        }

        private void Refresh(object sender)
        {
            var binding = BindingContext;
            BindingContext = null;
            BindingContext = binding;

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Browse, Title=viewModel.lc_007 },
                new HomeMenuItem {Id = MenuItemType.About, Title=viewModel.lc_005 }
            };

            ListViewMenu.ItemsSource = menuItems;
        }

        private void OnSelectLanguage(object sender, EventArgs e)
        {
            if(sender is Picker picker && picker.SelectedItem is Language lang)
                viewModel.RefreshLanguage(lang.Index);
        }
    }
}