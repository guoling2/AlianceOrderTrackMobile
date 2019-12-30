using XamarinSharedLibrary.Model;

namespace AlianceOrderTrackMobile.ViewModel.Model
{
    public class PackgeDriverDetail: ExtendedBindableObject

    {
        private string _userId;
        private string _realName;
        private string _carNumber;
        private string _mobileNumber;
        private string _serverStatuedDesc;
        public string UserId
        {
            get => _userId;

            set
            {
                _userId = value;
                RaisePropertyChanged(() => UserId);
            }
        }

        public string RealName
        {
            get => _realName;

            set
            {
                _realName = value;
                RaisePropertyChanged(() => RealName);
            }
        }

        public string CarNumber
        {
            get => _carNumber;

            set
            {
                _carNumber = value;
                RaisePropertyChanged(() => CarNumber);
            }
        }

        public string MobileNumber
        {
            get => _mobileNumber;

            set
            {
                _mobileNumber = value;
                RaisePropertyChanged(() => MobileNumber);
            }
        }
        public string ServerStatuedDesc
        {
            get => _serverStatuedDesc;

            set
            {
                _serverStatuedDesc = value;
                RaisePropertyChanged(() => ServerStatuedDesc);
            }
        }


        public string Display => RealName + "/" + MobileNumber;
    }
}
