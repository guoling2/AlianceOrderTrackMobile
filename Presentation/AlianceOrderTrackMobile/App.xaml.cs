using AlianceOrderTrackMobile.Page;
using System;
using System.Net;
using System.Net.Security;
using FormsToolkit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSharedLibrary.Help;
using XamarinSharedLibrary.IdentityModel;
using AlianceOrderTrackMobile.Views;
using XamarinSharedLibrary.Controls;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AlianceOrderTrackMobile
{
    public partial class App : Application
    {
        public App()
        {
            ServicePointManager.SetTcpKeepAlive(true, 30,10);
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicy) =>
            {
                if (sslPolicy == SslPolicyErrors.None)
                    return true;

                if (sslPolicy == SslPolicyErrors.RemoteCertificateChainErrors &&
                    ((HttpWebRequest)sender).RequestUri.AbsoluteUri.Equals("a trusted URL"))
                    return true;

                return true;
            };

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDU1OTRAMzEzNjJlMzMyZTMwVm1TSi8xN05KVm1jWHRlSFdKYzlRTSs4RHpMNjlCbDlsYU1xdGlBYjJOST0=");
            //  Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDEwMDVAMzEzNjJlMzMyZTMwanlVTTMrZHVIdkRHb2laemhQdExkNWxjSjhMS0FPNjNMUDBRU29GdjlGST0=");


            InitializeComponent();

            //  LoginDaniaPage
            DriverApp.RegisterUrlResource();
            DriverApp.RegisterDependencies();

            MainPage = new MyDashbordEx();
            //  MainPage = new LoginDaniaPage();

            // MainPage = new LoginDaniaPage();
        }
        protected override void OnStart()
        {
            OnResume();
        }
        protected override void OnResume()
        {


            if (GlobalSetting.Instance.UserToken != null)
            {
                return;
            }

            UnsubscribeMessage();

            MessagingService.Current.Subscribe<MessagingServiceAlert>(MessageKeys.Message, async (m, info) =>
            {
                var task = Application.Current?.MainPage?.DisplayAlert(info.Title, info.Message, info.Cancel);

                if (task == null)
                    return;

                await task;
                info?.OnCompleted?.Invoke();
            });

            MessagingService.Current.Subscribe(MessageKeys.NavigateLogin, async m =>
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    // ((MyDashbordTab) MainPage).IsVisible = false;

                    ((MyDashbordEx)MainPage).IsPresented = false;
                }

                Xamarin.Forms.Page page = null;
                if (GlobalSetting.Instance.UserToken==null && Device.RuntimePlatform== Device.Android)
                    page = new LoginDaniaPage();
                else
                    page = new SNavigationPage(new LoginDaniaPage());


                var nav = Application.Current?.MainPage?.Navigation;
                if (nav == null)
                    return;

                await NavigationService.PushModalAsync(nav, page);

            });



        }

        private void UnsubscribeMessage()
        {
            MessagingService.Current.Unsubscribe(MessageKeys.NavigateLogin);
            MessagingService.Current.Unsubscribe<MessagingServiceQuestion>(MessageKeys.Question);
            MessagingService.Current.Unsubscribe<MessagingServiceAlert>(MessageKeys.Message);
            MessagingService.Current.Unsubscribe<MessagingServiceChoice>(MessageKeys.Choice);
        }
        protected override void OnSleep()
        { 

            UnsubscribeMessage();

            // Handle when your app sleeps
           // CrossConnectivity.Current.ConnectivityChanged -= ConnectivityChanged;
        }
    }
}
