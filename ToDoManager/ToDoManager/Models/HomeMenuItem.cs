using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoManager.Models
{
    public enum MenuItemType
    {
        Browse,
        Stats,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
