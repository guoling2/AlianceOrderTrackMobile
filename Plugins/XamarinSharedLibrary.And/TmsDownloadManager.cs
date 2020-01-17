using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace XamarinSharedLibrary.And
{
    public class TmsDownloadManager
    {

        private long enqueeId;

        DownloadManager downloadManager;

        AppCompatActivity activity;


        //  private string savedir;

        //  private string savefilename;
        public TmsDownloadManager(AppCompatActivity activity)
        {
            downloadManager = (DownloadManager)activity.GetSystemService(Context.DownloadService);
            this.activity = activity;
            activity.RegisterReceiver(CreateDownloadReceiver(),
             new IntentFilter(DownloadManager.ActionDownloadComplete));


            //  savedir =Android.OS. Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
        }


        private DownloadReceiver CreateDownloadReceiver()
        {

            var x = new DownloadReceiver(downloadManager, activity);


            x.ComplageAction += Action;
            return x;

        }
        public void DownloadAndInstallApp(string downloadurl)
        {
            var savefilename = downloadurl.Substring(downloadurl.LastIndexOf("/", StringComparison.Ordinal) + 1);

            //  var path = Path.Combine(savedir, savefilename);


            DownloadManager.Request request =
                new DownloadManager.Request(Android.Net.Uri.Parse(downloadurl));
            request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, savefilename);
            // request.SetDestinationInExternalFilesDir(Application.Context, Android.OS.Environment.DirectoryDownloads, update. DownloadFileName);
            request.SetMimeType("application/vnd.android.package-archive");
            enqueeId = downloadManager.Enqueue(request);
        }

        private void Action(Context context, Intent intent)
        {
            DownloadManager.Query query = new DownloadManager.Query();

            long id = intent.GetLongExtra(DownloadManager.ExtraDownloadId, 0);
            query.SetFilterById(id);
            var c = downloadManager.InvokeQuery(query);
            if (c.MoveToFirst())
            {

                var filename = c.GetString(c.GetColumnIndex(DownloadManager.ColumnLocalUri));

                //  var path = Path.Combine(pathx.AbsolutePath, "com.chuanda.drivermobile.apk");
                var xxxxx = Java.Net.URI.Create(filename);


                var mApkFile = new Java.IO.File(Java.Net.URI.Create(filename));

                if (mApkFile.Exists())
                {
                    InstallApp(mApkFile, context);
                }

            }
        }

        private bool downLoadMangerIsEnable(Context context)
        {
            var state = context.ApplicationContext.PackageManager
                .GetApplicationEnabledSetting("com.android.providers.downloads");

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                return !(state == ComponentEnabledState.Disabled ||
                         state == ComponentEnabledState.Enabled
                         || state == ComponentEnabledState.DisabledUntilUsed);
            }
            else
            {
                return !(state == ComponentEnabledState.Disabled ||
                         state == ComponentEnabledState.DisabledUser);
            }
        }
        public void InstallApp(Java.IO.File mApkFile, Context context)
        {
            bool isInstallPermission = false;//是否有8.0安装权限


            var xx = downLoadMangerIsEnable(context);
            //if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            //{
            //    isInstallPermission = getPackageManager().canRequestPackageInstalls();
            //}

            //   var mApkFile = new Java.IO.File(filename);

            Android.Net.Uri mUri = null;

            if (mApkFile.Exists())
            {
                Intent installApkIntent = new Intent(Intent.ActionView);

                installApkIntent.AddFlags(ActivityFlags.NewTask);
                installApkIntent.SetAction(Intent.ActionView);

                installApkIntent.AddFlags(ActivityFlags.GrantPersistableUriPermission
                                          | ActivityFlags.GrantReadUriPermission
                                          | ActivityFlags.GrantWriteUriPermission);

                if (Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.N)
                {
                    mUri = FileProvider.GetUriForFile(context, activity.PackageName + ".fileprovider", mApkFile);

                    //installApkIntent.SetDataAndType(mUri, "application/vnd.android.package-archive");
                    //activity.StartActivity(installApkIntent);
                }
                else
                {
                    mUri = Android.Net.Uri.FromFile(mApkFile);

                }
                installApkIntent.SetDataAndType(mUri, "application/vnd.android.package-archive");
                context.StartActivity(installApkIntent);
            }

        }
    }



    public interface IDownloadReceiverComplate
    {


        void Action(Context context, Intent intent);
    }


    public class DownloadReceiver : BroadcastReceiver
    {
        DownloadManager downloadManager;

        AppCompatActivity activity;

        // IDownloadReceiverComplate receiverComplate;


        public Action<Context, Intent> ComplageAction
        {
            get;
            set;
        }
        public DownloadReceiver(DownloadManager downloadManager, AppCompatActivity activity)
        {
            this.downloadManager = downloadManager;
            this.activity = activity;
            //  this.receiverComplate = receiverComplate;
        }
        public override void OnReceive(Context context, Intent intent)
        {


            if (ComplageAction != null)
            {

                ComplageAction(context, intent);
                // receiverComplate.Action(context, intent);
            }



        }
    }
}