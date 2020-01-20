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

namespace XamarinSharedLibrary.And.Broadcast
{
    public class BarcodeReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string data = intent.GetStringExtra("SCAN_BARCODE1");
        }
    }

}