using System;

using ToDoManager.Models;

namespace ToDoManager.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Label;
            Item = item;
        }
    }
}
