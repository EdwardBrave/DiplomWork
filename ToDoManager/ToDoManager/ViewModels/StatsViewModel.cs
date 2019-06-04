using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using ToDoManager.Models;
using ToDoManager.Datas;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToDoManager.ViewModels
{

    class CategoryData
    {
        public Category Category;
        public List<ArchiveItem> Tasks;
    }


    public class StatsViewModel : BaseViewModel
    {
        public ObservableCollection<CategoryStats> Statistics { get; set; }
        public Command LoadStatsCommand { get; set; }

        public string lc_020 => Locale.Tr("lc_020");

        public StatsViewModel()
        {
            Statistics = new ObservableCollection<CategoryStats>();
            LoadStatsCommand = new Command(async () => await UpdateStatistics());
            UpdateStatistics();
        }

        public async Task UpdateStatistics()
        {
            var categoriesData = await CategoriesSortedData();
            Statistics.Clear();
            foreach (var categiryData in categoriesData)
            {
                Statistics.Add(new CategoryStats()
                {
                    Title   = categiryData.Category.Name,
                    Section = categiryData.Category.Section,
                    Count   = categiryData.Tasks.Count
                });
            }
        }

        private async Task<List<CategoryData>> CategoriesSortedData()
        {
            var categoryDatas = new List<CategoryData>();
            var items = await ArchiveStore.GetItemsAsync();
            List<Category> categories;
            if (DataStore is DataBaseStore dataBaseStore)
            {
                categories = await dataBaseStore.GetCateoriesAsync() as List<Category>;
                foreach (var category in categories)
                {
                    var categoryData = new CategoryData() { Category = category };
                    var outData = items.Where(obj => obj.Category == category.Name).Select(obj => obj);
                    categoryData.Tasks = new List<ArchiveItem>(outData);
                    categoryDatas.Add(categoryData);
                }
            }
            else
                return null;
            
            return await Task.FromResult(categoryDatas);
        }
    }
}
