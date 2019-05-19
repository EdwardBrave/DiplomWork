using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoManager.Models;

namespace ToDoManager.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        private SQLiteAsyncConnection dataBase;

        public MockDataStore()
        {
            items = new List<Item>();
            string dbPath = System.IO.Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
               "ToDoData.db3");
            string str = Locale.Tr("lc_001");
            dataBase = new SQLiteAsyncConnection(dbPath);
            dataBase.CreateTableAsync<Item>();
            RefreshCategory();
        
        }

        private async void RefreshCategory()
        {
            await dataBase.CreateTableAsync<Category>();
            if (await dataBase.Table<Category>().CountAsync() <= 0)
            {
                await dataBase.InsertAllAsync(new List<Category>() {
                    new Category() { Name = "Sport", Section = "Health" },
                    new Category() { Name = "Food", Section = "Health" },
                    new Category() { Name = "Dream", Section = "Health" },

                    new Category() { Name = "Passive", Section = "Relaxation" },
                    new Category() { Name = "Hobby", Section = "Relaxation" },
                    new Category() { Name = "Active", Section = "Relaxation" },

                    new Category() { Name = "Study", Section = "Work" },
                    new Category() { Name = "Job", Section = "Work" },
                    new Category() { Name = "Business", Section = "Work" }
                });
            }
        }

        ~MockDataStore()
        {
            if (dataBase != null)
                dataBase.CloseAsync();
        }

        public async Task<int> AddItemAsync(Item item)
        {
            if (item.Id == null)
            {
                var itemsList = await dataBase.QueryAsync<Item>("SELECT * FROM Tasks ORDER BY Id DESC LIMIT 1");
                item.Id = (itemsList.Count == 0) ? "1" : 
                    (int.Parse(itemsList[0].Id, System.Globalization.NumberStyles.HexNumber) + 1).ToString();
            }
            return await dataBase.InsertAsync(item);
        }

        public async Task<int> UpdateItemAsync(Item item)
        {
            await dataBase.DeleteAsync<Item>(item.Id);     
            return await dataBase.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(string id)
        {
            return await dataBase.DeleteAsync<Item>(id);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await dataBase.FindAsync<Item>(id);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await dataBase.QueryAsync<Item>("select * from Tasks");
        }
    }
}