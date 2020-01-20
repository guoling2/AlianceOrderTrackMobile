using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TmsBuinessCommonLibrary.Services;
using Xamarin.Forms;
using XamarinSharedLibrary.Broadcast;

namespace TmsBuinessCommonLibrary.Help
{
    public static class BroadcastHelp
    {

        public static async Task SetBroadcast(this IBroadcastService broadcastService)
        {
            var broadcastConfigService = Xamarin.Forms.DependencyService.Resolve<BroadcastConfigService>();
            var dbresult =  broadcastConfigService.GetById(Xamarin.Essentials.DeviceInfo.Model);

            if (dbresult != null)
            {
                if (string.IsNullOrWhiteSpace(dbresult.FileActionName) ||
                    string.IsNullOrWhiteSpace(dbresult.BarCodeName))
                {
                    return;
                }
                var broadcastModel = new BroadcastModel
                {
                    ActionName = dbresult.FileActionName,
                    StringExtra = dbresult.BarCodeName
                };

                broadcastService.Init(broadcastModel);
            }



        }
        public static IBroadcastService CreateBarcode1Service()
        {

            var broadcastService = Xamarin.Forms.DependencyService.Resolve<IBroadcastService>(DependencyFetchTarget.NewInstance);



            return broadcastService;
        }
    }
}
