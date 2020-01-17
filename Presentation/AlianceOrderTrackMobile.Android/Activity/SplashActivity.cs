using System;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Droid.Services;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using XamarinSharedLibrary.IdentityModel;

namespace AlianceOrderTrackMobile.Droid.Activity
{

    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]

    public class SplashActivity : AppCompatActivity
    {

        static readonly string TAG = "X:" + typeof(SplashActivity).Name;
        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);
        //    var intent = new Intent(this, typeof(MainActivity));
        //    StartActivity(intent);
        //    Finish();
        //}

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(async () => { await SimulateStartup(); });
            startupWork.Start();
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        // Simulates background work that happens behind the splash screen
        async Task SimulateStartup()
        {
           
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");

            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this);
            try
            {
                AppUpdateService asc = new AppUpdateService();

                var model = await asc.GetServicesVersion();

                if (model != null)
                {


                    GlobalSetting.Instance.ServicesVersion = model.NowVersion;
                    GlobalSetting.Instance.UpdateMsg = model.UpdateMessage;
                    GlobalSetting.Instance.UpdateDownloadUrl = model.UpdateUrl;
                }
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
                //Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            }
            catch (Exception ex)
            {

        

                builder.SetTitle("启动失败");
                builder.SetMessage("由于远程服务器错误，启动失败！");
                builder.SetNeutralButton("确定",(IDialogInterfaceOnClickListener)null);
                builder.Show();
                Log.Error(TAG, ex.Message);

                throw;
            }
           
          

         


          
          
        }
    }
}