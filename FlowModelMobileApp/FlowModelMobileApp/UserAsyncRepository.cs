using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace FlowModelMobileApp
{
   public class UserAsyncRepository
   {
      SQLiteAsyncConnection database;

      public UserAsyncRepository(string databasePath)
      {
         database = new SQLiteAsyncConnection(databasePath);
      }

      public async Task CreateTable()
      {
         await database.CreateTableAsync<Users>();
      }
      public async Task<List<Users>> GetItemsAsync()
      {
         return await database.Table<Users>().ToListAsync();

      }
      public async Task<Users> GetItemAsync(int id)
      {
         return await database.GetAsync<Users>(id);
      }
      public async Task<int> DeleteItemAsync(Users item)
      {
         return await database.DeleteAsync(item);
      }
      public async Task<int> SaveItemAsync(Users item)
      {
         if (item.UserId != 0)
         {
            await database.UpdateAsync(item);
            return item.UserId;
         }
         else
         {
            return await database.InsertAsync(item);
         }
      }
   }
}
