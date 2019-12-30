using AlianceOrderTrackMobile.Services;
using AlianceOrderTrackMobile.Services.Abstractions;
using AlianceOrderTrackMobile.Services.Impl;
using XamarinSharedLibrary.IdentityModel;
using XamarinSharedLibrary.Model.Token;

namespace AlianceOrderTrackMobile
{
   public static class DriverApp
    {

       
        public static void RegisterDependencies()
        {



            GlobalSetting.Instance.BaseIdentityEndpoint = "https://account.trandawl.cn";
            GlobalSetting.Instance.GatewayLogisticEndpoint = "https://aliance.trandawl.cn";
            


            IdentityServicesExtension.Register();

            Xamarin.Forms.DependencyService.Register<IdentityService>();

            Xamarin.Forms.DependencyService.Register<IRequestProvider, XRequestProvider>();

            Xamarin.Forms.DependencyService.Register<ILogisticStoreServices,LogisticStoreServices>();

            Xamarin.Forms.DependencyService.Register<IShipmentServices, ShipmentService>();

            Xamarin.Forms.DependencyService.Register<IDriverUserServices, DriverUserServices>();

            Xamarin.Forms.DependencyService.Register<IShipmentPlanServices, ShipmentPlanServices>();

            //if (Settings.Current.UseLocalDev)
            //{
            //    Settings.Current.UrlBase = "http://onlineorder_test.trandawl.cn";
            //}
        }
    }
}
