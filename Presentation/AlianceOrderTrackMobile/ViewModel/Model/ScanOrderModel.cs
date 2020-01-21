using XamarinSharedLibrary.Model;

namespace AlianceOrderTrackMobile.ViewModel.Model
{
    public class ScanOrderModel: ExtendedBindableObject
    {

        private string _taskId;
        private string _serverId;
        private decimal _ordercount;
        private string _serverStatued;


        public string TaskId
        {
            get => _taskId;

            set
            {
                _taskId = value;
                RaisePropertyChanged(() => TaskId);
            }
        }
        public string ServerId
        {
            get => _serverId;

            set
            {
                _serverId = value;
                RaisePropertyChanged(() => ServerId);
            }
        }

        public decimal OrderCount
        {
            get => _ordercount;

            set
            {
                _ordercount = value;
                RaisePropertyChanged(() => OrderCount);
            }
        }

        public string ServerStatued
        {
            get => _serverStatued;

            set
            {
                _serverStatued = value;
                RaisePropertyChanged(() => ServerStatued);
            }
        }
    }
}
