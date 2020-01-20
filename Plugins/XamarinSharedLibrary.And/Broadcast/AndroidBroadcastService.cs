using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using XamarinSharedLibrary.And.Broadcast;
using XamarinSharedLibrary.Broadcast;

[assembly: Dependency(typeof(AndroidBroadcastService))]
namespace XamarinSharedLibrary.And.Broadcast
{
    public class AndroidBroadcastService : IBroadcastService
    {
        private IntentFilter intentFilter;
        private CommonBarcodeReceiver networkChangeReceiver;

        public event EventHandler<BroadcastReceiveEventArgs> Result;

        private bool register = false;
        public void Init(BroadcastModel broadcast)
        {
            intentFilter = new IntentFilter();
            //  intentFilter.AddAction("nlscan.action.SCANNER_RESULT");

            intentFilter.AddAction(broadcast.ActionName);
            networkChangeReceiver = new CommonBarcodeReceiver(broadcast)
            {
                ResultEvent = (a, b) =>
                {
                    if (Result != null)
                    {
                        b.Result = b.Result.Trim();
                        Result(this, b);
                    }
                }
            };

            register = true;
            CrossCurrentActivity.Current.Activity.RegisterReceiver(networkChangeReceiver, intentFilter);
            // this.RegisterReceiver(networkChangeReceiver, intentFilter);
        }

        public void UnregisterReceiver()
        {
            if (register == true)
            {
                CrossCurrentActivity.Current.Activity.UnregisterReceiver(networkChangeReceiver);
            }

        }
    }
}