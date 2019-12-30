using XamarinSharedLibrary.Model;

namespace AlianceOrderTrackMobile.ViewModel.Model
{
    public class ScanOrderModel: ExtendedBindableObject
    {

        private string _taskId;
        private string _serverId;
        private string _toCity;
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

        public string ToCity
        {
            get => _toCity;

            set
            {
                _toCity = value;
                RaisePropertyChanged(() => ToCity);
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
