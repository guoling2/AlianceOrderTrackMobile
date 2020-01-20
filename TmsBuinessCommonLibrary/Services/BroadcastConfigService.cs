using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsBuinessCommonLibrary.Model;
using XamarinSharedLibrary.Sqllite;

namespace TmsBuinessCommonLibrary.Services
{
    public class BroadcastConfigService: TmsDatabase<BroadcastConfigModel>
    {
        public static void Register(bool ismock = false)
        {


            Xamarin.Forms.DependencyService.Register<BroadcastConfigService>();
        }

      
        public BroadcastConfigService():base()
        {

            CreateTable();
        }

        public BroadcastConfigModel GetById(string name)
        {
            try
            {
                try
                {
                    var result =  GetAsync(t => t.MachineId == name);

                    return result.FirstOrDefault();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
               
            //  var datareader=  _tmsLocalDbContext.GetSingel<BroadcastConfigModel>(name, "BroadcastConfig", "Id");

              //BroadcastConfigModel broadcastConfig = new BroadcastConfigModel();

              //if (await datareader.NextResultAsync())
              //{
              //    broadcastConfig.Id = datareader["Id"].ToString();

              //    broadcastConfig.BarCodeName = datareader["BarCodeName"].ToString();

              //    broadcastConfig.FileActionName = datareader["FileActionName"].ToString();
              //  }
             //   var x = await sqLiteHelper.Table<BroadcastConfigModel>().ToListAsync();
            // return datareader;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }




        }

        public Task Update(BroadcastConfigModel broadcastConfigModel)
        {
            return UpdateAsync(broadcastConfigModel);
        }

        public Task Insert(BroadcastConfigModel broadcastConfigModel)
        {
           return  InsertAsync(broadcastConfigModel);
        }
        //public Task<int> Delete()
        //{
        //    return sqLiteHelper.DeleteAllAsync<BroadcastConfigModel>();

        //}

        //public Task<int> InsertAsync(BroadcastConfigModel item)
        //{
        //    return sqLiteHelper.InsertAsync(item);

        //}
    }


}
