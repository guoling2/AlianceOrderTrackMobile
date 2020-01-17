using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Square.OkHttp3;
using TmsBuinessCommonLibrary.Model;
using XamarinSharedLibrary.And;
using XamarinSharedLibrary.And.Ssl;
using XamarinSharedLibrary.IdentityModel;

namespace AlianceOrderTrackMobile.Droid.Services
{
    public class AppUpdateService
    {

        public async Task<AppUpdateModel> GetServicesVersion()
        {




            var searchurl = GlobalSetting.Instance.GatewayLogisticEndpoint + "/api/App/get?id=Android_TmsLogisticerApp";



            OkHttpClient client = new OkHttpClient();
            OkHttpClient mOkHttpClient = client.NewBuilder()
                .SslSocketFactory(HttpsTrustManager.createSSLSocketFactory(), new HttpsTrustManager())
                .HostnameVerifier(new TrustAllHostnameVerifier())
                .Build();

            Request request = new Request.Builder().Url(searchurl).Get().Build();
            ICall call = mOkHttpClient.NewCall(request);


            var response = await call.ExecuteAsync();

            var content = await response.Body().StringAsync();

            var appupdatemodel = JsonConvert.DeserializeObject<AppUpdateModel>(content);

            return appupdatemodel;
        }




        public static void UpdateCheckVersionAsync(AppCompatActivity activity)
        {





            Button yesBtn;

            Android.Support.V7.App.AlertDialog updatadialog;


            var xxx = activity.PackageManager.GetPackageInfo(activity.PackageName, 0).VersionName;


            // HttpClient hc = new HttpClient();

            if (string.IsNullOrWhiteSpace(GlobalSetting.Instance.ServicesVersion))
            {

                return;
            }

            var appupdatemodel = new AppUpdateModel()
            {
                NowVersion = GlobalSetting.Instance.ServicesVersion,
                UpdateMessage = GlobalSetting.Instance.UpdateMsg,
                UpdateUrl = GlobalSetting.Instance.UpdateDownloadUrl
            };

            if (appupdatemodel.NowVersion == xxx)
            {
                return;
            }



            ISpanned x;
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.N)
            {
                x = Html.FromHtml(appupdatemodel.UpdateMessage, FromHtmlOptions.ModeLegacy);
            }
            else
            {

                x = Html.FromHtml(appupdatemodel.UpdateMessage);
                // charSequence = Html.FromHtml(content);
            }

            var builder = new Android.Support.V7.App.AlertDialog.Builder(activity);

            builder.SetCancelable(false);
            builder.SetTitle("更新提示!");
            builder.SetMessage(x);
            // Create empty event handlers, we will override them manually instead of letting the builder handling the clicks.
            builder.SetPositiveButton("更新", (IDialogInterfaceOnClickListener)null);
            //   builder.SetNegativeButton("No", (EventHandler<DialogClickEventArgs>)null);
            updatadialog = builder.Create();

            // Show the dialog. This is important to do before accessing the buttons.


            updatadialog.Show();

            // Get the buttons.
            yesBtn = updatadialog.GetButton((int)DialogButtonType.Positive);
            // var noBtn = dialog.GetButton((int)DialogButtonType.Negative);

            // Assign our handlers.
            yesBtn.Click += (sender, args) =>
            {

                yesBtn.Enabled = false;

                updatadialog.SetMessage("正在下载新版本。。。");


                TmsDownloadManager tm = new TmsDownloadManager(activity);

                tm.DownloadAndInstallApp(appupdatemodel.UpdateUrl);

            };

        }
    }
}