using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.ViewModel;
using FormsToolkit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSharedLibrary.Help;
using XamarinSharedLibrary.IdentityModel;

namespace AlianceOrderTrackMobile.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MissionTabbedPage : TabbedPage
    {
        public MissionTabbedPage ()
        {
            InitializeComponent();


            this.BindingContext = new MissionDashbordViewModel(this.Navigation);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            ReloadUserToken();
        }

        private void ReloadUserToken()
        {


            var usertoken = GlobalSetting.Instance.UserToken;

            if (usertoken == null)
            {
                MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            }
            else
            {
                //if()
                //MessagingService.Current.SendMessage(Settings.Current.LastExpirestimeteTime > DateTime.UtcNow
                //    ? MessageKeys.LoggedIn
                //    : MessageKeys.NavigateLogin);
            }
        }
    }
}