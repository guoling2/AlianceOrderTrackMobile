using System;
using Acr.UserDialogs;
using AlianceOrderTrackMobile.Droid.Services;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using FormsToolkit.Droid;
using Plugin.Permissions;
using XamarinSharedLibrary.And.Files.PCLStorage;

namespace AlianceOrderTrackMobile.Droid.Activity
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;


            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);


            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);

            UserDialogs.Init(this);

            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            Toolkit.Init();

            UserDialogs.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            LoadApplication(new App());

            AppUpdateService.UpdateCheckVersionAsync(this);
       

        }

        public override Intent RegisterReceiver(BroadcastReceiver receiver, IntentFilter filter)
        {
           
            return base.RegisterReceiver(receiver, filter);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            //if (requestCode == TmsAndroidPromise.AMapLocation)
            //{
            //    foreach (Permission grantResult in grantResults)
            //    {
            //        if (grantResult == Permission.Denied)
            //        {


            //            //    var builder = new Android.Support.V7.App.AlertDialog.Builder(this);

            //            //    builder.SetCancelable(false);
            //            ////    AlertDialog.Builder builder = new AlertDialog.Builder(this);
            //            //    builder.SetTitle("权限设置");
            //            //    builder.SetMessage("系统需要定位权限才能工作");
            //            //    builder.SetPositiveButton("获取权限", (a,b)=>
            //            //    {
            //            //        this.StartActivity(PermissionUtil.getAppDetailSettingIntent(this));
            //            //        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            //            //    });
            //            //    builder.SetNegativeButton("取消", (a, b) =>
            //            //    {
            //            //        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            //            //    });
            //            //    builder.Show();

            //            //    break;
            //            //  Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            //            //android.os.Process.killProcess(android.os.Process.myPid());
            //            //var xxx = System.Environment.ExitCode;

            //            //System.Environment.Exit(System.Environment.ExitCode);
            //            //AlertDialog.Builder builder = new AlertDialog.Builder(this);
            //            //builder.SetTitle("权限设置");


            //            //Intent localIntent = new Intent();
            //            //localIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            //            //localIntent.setAction("android.settings.APPLICATION_DETAILS_SETTINGS");
            //            //localIntent.setData(Uri.fromParts("package", context.getPackageName(), null));

            //            //return localIntent;
            //            //    ————————————————
            //            //版权声明：本文为CSDN博主「鹏鹏~」的原创文章，遵循 CC 4.0 BY - SA 版权协议，转载请附上原文出处链接及本声明。
            //            //原文链接：https://blog.csdn.net/qq_36321889/article/details/90404083
            //            //System.Environment.Exit(0);

            //            //退出
            //        }
            //    }
            //}


            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            FolderAccessPermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }


    public class ScanBarEvent: KeyEvent
    {
        protected ScanBarEvent(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public ScanBarEvent(KeyEvent origEvent) : base(origEvent)
        {
        }

        public ScanBarEvent(KeyEvent origEvent, long eventTime, int newRepeat) : base(origEvent, eventTime, newRepeat)
        {
        }

        public ScanBarEvent(KeyEventActions action, Keycode code) : base(action, code)
        {
        }

        public ScanBarEvent(long time, string characters, int deviceId, KeyEventFlags flags) : base(time, characters, deviceId, flags)
        {
        }

        public ScanBarEvent(long downTime, long eventTime, KeyEventActions action, Keycode code, int repeat) : base(downTime, eventTime, action, code, repeat)
        {
        }

        public ScanBarEvent(long downTime, long eventTime, KeyEventActions action, Keycode code, int repeat, MetaKeyStates metaState) : base(downTime, eventTime, action, code, repeat, metaState)
        {
        }

        public ScanBarEvent(long downTime, long eventTime, KeyEventActions action, Keycode code, int repeat, MetaKeyStates metaState, int deviceId, int scancode) : base(downTime, eventTime, action, code, repeat, metaState, deviceId, scancode)
        {
        }

        public ScanBarEvent(long downTime, long eventTime, KeyEventActions action, Keycode code, int repeat, MetaKeyStates metaState, int deviceId, int scancode, KeyEventFlags flags) : base(downTime, eventTime, action, code, repeat, metaState, deviceId, scancode, flags)
        {
        }

        public ScanBarEvent(long downTime, long eventTime, KeyEventActions action, Keycode code, int repeat, MetaKeyStates metaState, int deviceId, int scancode, KeyEventFlags flags, InputSourceType source) : base(downTime, eventTime, action, code, repeat, metaState, deviceId, scancode, flags, source)
        {
        }
    }
}