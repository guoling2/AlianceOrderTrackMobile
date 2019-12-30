using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;

namespace AlianceOrderTrackMobile.Droid
{
    //You can specify additional application information in this attribute

#if DEBUG
    [Application(Debuggable = true)]
#else
	[Application(Debuggable = false)]
#endif
    public class MainApplication : Application
    {



        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          :base(handle, transer)
        {
          
           
            //WebView wv = new WebView(this.ApplicationContext);
            //wv.Settings.DatabaseEnabled
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);
        }

    }


   
}