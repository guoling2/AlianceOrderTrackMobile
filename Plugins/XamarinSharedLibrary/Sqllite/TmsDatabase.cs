using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace XamarinSharedLibrary.Sqllite
{
    public class TmsDatabase<T> where T: TmsSqLiteModel,new()
    {

        readonly SQLiteAsyncConnection _database;


        public TmsDatabase() : this("TmsLocalDB.db3") { }
        public TmsDatabase(string dbname)
        {
            var x = Xamarin.Forms.DependencyService.Resolve<ISqliteDbInterface>(Xamarin.Forms.DependencyFetchTarget
                .GlobalInstance);

          var dbpath=  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbname);

            _database = new SQLiteAsyncConnection(dbpath);
           
        }

        public void CreateTable()
        {
            try
            {
                _database.CreateTableAsync(typeof(T)).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        
        }
           

        public Task<List<T>> GetAsync()
        {
            return _database.Table<T>().ToListAsync();
        }

        public List<T> GetAsync(Expression<Func<T, bool>> predExpr)
        {

            return   _database.Table<T>()
                .Where(predExpr).ToListAsync().GetAwaiter().GetResult(); ;

        }

        public Task<int> UpdateAsync(T model)
        {

            return _database.UpdateAsync(model);

            //if (string.IsNullOrWhiteSpace(model.ToString()))
            //{
            //    return _database.UpdateAsync(model);
            //}
            //else
            //{

            //}
        }

        public Task<int> InsertAsync(T model)
        {

            return _database.InsertAsync(model);

            //if (string.IsNullOrWhiteSpace(model.ToString()))
            //{
            //    return _database.UpdateAsync(model);
            //}
            //else
            //{
               
            //}
        }

        public Task<int> DeleteAsync(string primaryKey)
        {
            return _database.DeleteAsync(primaryKey);
        }
    }
}
