using AlianceOrderTrackMobile.Services;
using AlianceOrderTrackMobile.Services.Abstractions;
using AlianceOrderTrackMobile.Services.Impl;
using TmsBuinessCommonLibrary.Services;
using XamarinSharedLibrary.IdentityModel;
using XamarinSharedLibrary.Model.Token;
using XamarinSharedLibrary.Sqllite;

namespace AlianceOrderTrackMobile
{
   public static class DriverApp
    {



        public static void RegisterUrlResource()
        {
            GlobalSetting.Instance.BaseIdentityEndpoint = "https://account.trandawl.cn";

            GlobalSetting.Instance.GatewayLogisticEndpoint = "https://aliance.trandawl.cn";

        }
        public static void RegisterDependencies()
        {



          //  Xamarin.Forms.DependencyService.Register<TmsLocalDbContext>();

            BroadcastConfigService.Register();

            IdentityServicesExtension.Register();

            Xamarin.Forms.DependencyService.Register<IdentityService>();


            Xamarin.Forms.DependencyService.Register<IShipmentXiecheServices, ShipmentXiecheServices>();

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
