using System;

namespace XamarinSharedLibrary.Broadcast
{
    public interface IBroadcastService
    {
        event EventHandler<BroadcastReceiveEventArgs> Result;

        void Init(BroadcastModel broadcast);
        void UnregisterReceiver();


         

    }

}
