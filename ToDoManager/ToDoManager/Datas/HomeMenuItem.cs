using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoManager.Datas
{
    public enum MenuItemType
    {
        Browse,
        Stats,
        Archive,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
