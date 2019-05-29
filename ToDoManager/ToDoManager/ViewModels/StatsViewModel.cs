using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using ToDoManager.Services;
using ToDoManager.Models;
using System.Threading.Tasks;

namespace ToDoManager.ViewModels
{
    public class CategoryStats
    {
        public string Title { get; set; }
        public string Section { get; set; }
        public int Count { get; set; }
    }

    class CategoryData
    {
        public Category Category;
        public List<ArchiveItem> Tasks;
    }


    public class StatsViewModel
    {
        public ObservableCollection<CategoryStats> Statistics { get; set; }

        public string lc_020 => Locale.Tr("lc_020");

        public StatsViewModel()
        {
            Statistics = new ObservableCollection<CategoryStats>();

        }

        private async Task<List<CategoryData>> CategoriesSortedData()
        {
            return null;
        }
    }
}
