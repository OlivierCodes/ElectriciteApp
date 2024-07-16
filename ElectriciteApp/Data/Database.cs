using ElectriciteApp.Models;
using ElectriciteApp.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectriciteApp.Data
{
    public class Database
    {
        private SQLiteAsyncConnection connection;
        public Database()
        { 


        }

        async Task Initialize()
        {
            if (connection is not null) 
                return;

            connection = new(Constants.DatabasePath);

              await connection.CreateTableAsync<Locataire>();

        }

        public async Task<List<Locataire>> GetItemsAsync()
        {
            await Initialize();

            return await connection.Table<Locataire>().ToListAsync();
        }

       
        public async Task<int> SaveItemAsync(Locataire item)
        {
            await Initialize();

            if(item.Id != 0)
            {
                return await connection.UpdateAsync(item);
            }
            else
            {
                return await connection.InsertAsync(item);
            }

        }


        public async Task<int> DeleteItemAsync(Locataire item)
        {
            await Initialize();
            return await connection.DeleteAsync(item);
        }



        

    }
}
