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
using XamarinSharedLibrary.Broadcast;

namespace XamarinSharedLibrary.And.Broadcast
{
    public class CommonBarcodeReceiver : BroadcastReceiver
    {
        public EventHandler<BroadcastReceiveEventArgs> ResultEvent { get; set; }
        private BroadcastModel broadcast;
        public CommonBarcodeReceiver(BroadcastModel broadcast)
        {

            this.broadcast = broadcast;



        }
        public override void OnReceive(Context context, Intent intent)
        {
            //var xcc= intent.Extras.KeySet();
            string data = intent.GetStringExtra(broadcast.StringExtra);

            ResultEvent(this, new BroadcastReceiveEventArgs()
            {
                Result = data
            });
            //eventHandler(this, new BroadcastReceiveEventArgs()
            //{
            //    Result = data
            //});
        }
    }
}