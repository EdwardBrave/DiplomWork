using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ToDoManager.Models;

namespace ToDoManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            Date = new DateTime();
            Time = new TimeSpan();
            Item = new Item
            {
                Label = "New task"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}