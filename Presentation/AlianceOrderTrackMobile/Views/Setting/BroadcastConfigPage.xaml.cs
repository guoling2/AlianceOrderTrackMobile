using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsBuinessCommonLibrary.Model;
using TmsBuinessCommonLibrary.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlianceOrderTrackMobile.Views.Setting
{
    public partial class BroadcastConfigPage : ContentPage, IXamarinPageInitialize
    {


        public BroadcastConfigModel BroadcastConfigModel { get; set; }
        private BroadcastConfigService broadcastConfigService;
        public BroadcastConfigPage()
        {
            InitializeComponent();

         
        }

        private async void ToolbarItem_Reload_Clicked(object sender, EventArgs e)
        {
            await Initialize();
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {

            if (BroadcastConfigModel == null)
            {
                BroadcastConfigModel = new BroadcastConfigModel
                {
                    MachineId = this.MachineTxt.Text,
                    FileActionName = this.FileActionName.Text,
                    BarCodeName = this.BarCodeName.Text
                };

                await broadcastConfigService.Insert(BroadcastConfigModel);

            }
            else
            {
               var model= this.BindingContext as BroadcastConfigModel;

               await broadcastConfigService.Update(model);

            }


            //try
            //{
            //    await broadcastConfigService.DeleteAsync(BroadcastConfigModel.MachineId);
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //    throw;
            //}




            await Acr.UserDialogs.UserDialogs.Instance.AlertAsync("配置完成");
        }

        public async Task Initialize()
        {
            broadcastConfigService = Xamarin.Forms.DependencyService.Resolve<BroadcastConfigService>();

        
            try
            {
                var config = broadcastConfigService.GetById(Xamarin.Essentials.DeviceInfo.Model);
                if (config != null)
                {
                    this.BindingContext = BroadcastConfigModel = new BroadcastConfigModel
                    {
                        MachineId = config.MachineId,
                        FileActionName = config.FileActionName,
                        BarCodeName = config.BarCodeName
                    };

                }
                else
                {
                    this.BindingContext  = new BroadcastConfigModel()
                    {
                        MachineId = Xamarin.Essentials.DeviceInfo.Model
                    };

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}