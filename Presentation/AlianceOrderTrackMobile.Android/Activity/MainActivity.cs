using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Plugin.Permissions;

namespace AlianceOrderTrackMobile.Droid.Activity
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

          
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();


            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //this.OnKeyDown(Android.Views.Keycode.Enter,
            //    new Android.Views.KeyEvent(KeyEventActions.Down, Android.Views.Keycode.Enter));

            base.OnCreate(savedInstanceState);

            LoadApplication(new App());
         

         //   LoadApplication(new App(),);
         
        }

        public override Intent RegisterReceiver(BroadcastReceiver receiver, IntentFilter filter)
        {
           
            return base.RegisterReceiver(receiver, filter);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
          
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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