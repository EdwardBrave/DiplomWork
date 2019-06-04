using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoManager.Datas;
using Xamarin.Forms;
using ToDoManager.Models;

[assembly: Dependency(typeof(DataArchiveStore))]
namespace ToDoManager.Models
{
    public class DataArchiveStore : IDataStore<ArchiveItem>
    {
        private SQLiteAsyncConnection dataBase;

        public DataArchiveStore()
        {
            string dbPath = System.IO.Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
               "ToDoData.db3");
            dataBase = new SQLiteAsyncConnection(dbPath);
            dataBase.CreateTableAsync<ArchiveItem>();
        
        }

        ~DataArchiveStore()
        {
            if (dataBase != null)
                dataBase.CloseAsync();
        }

        public async Task<int> AddItemAsync(ArchiveItem item)
        {
            if (item.Id == null)
            {
                var itemsList = await dataBase.QueryAsync<ArchiveItem>("SELECT * FROM ArchiveTasks ORDER BY Id DESC LIMIT 1");
                item.Id = (itemsList.Count == 0) ? "1" : 
                    (int.Parse(itemsList[0].Id, System.Globalization.NumberStyles.HexNumber) + 1).ToString();
            }
            return await dataBase.InsertAsync(item);
        }

        public async Task<int> UpdateItemAsync(ArchiveItem item)
        {
            await dataBase.DeleteAsync<ArchiveItem>(item.Id);     
            return await dataBase.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(string id)
        {
            return await dataBase.DeleteAsync<ArchiveItem>(id);
        }

        public async Task<ArchiveItem> GetItemAsync(string id)
        {
            return await dataBase.FindAsync<ArchiveItem>(id);
        }

        public async Task<IEnumerable<ArchiveItem>> GetItemsAsync(bool forceRefresh = false)
        {
            var items = await dataBase.QueryAsync<ArchiveItem>("select * from ArchiveTasks");
            var list = from obj in items
                    orderby (obj.Priority = (obj.Importance + obj.Urgency) / 2F) descending
                    select obj;
            return list;
        }
    }
}