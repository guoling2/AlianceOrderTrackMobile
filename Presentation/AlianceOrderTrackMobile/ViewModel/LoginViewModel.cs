using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FormsToolkit;
using Xamarin.Forms;
using XamarinSharedLibrary.Help;
using XamarinSharedLibrary.IdentityModel;
using XamarinSharedLibrary.Model;
using XamarinSharedLibrary.Model.Token;

namespace AlianceOrderTrackMobile.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {


        public LoginViewModel(INavigation navigation) : base(navigation)
        {

            Message = "登录";
            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var _item = item as Item;
            //    Items.Add(_item);
            //    await DataStore.AddItemAsync(_item);
            //});
        }
        string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }
        string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }


        ICommand _loginCommand;
        public ICommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new Command(async () => await ExecuteLoginAsync()));


        ICommand _signupCommand;

        public ICommand SignupCommand =>
            _signupCommand ?? (_signupCommand = new Command(async () => await ExecuteSignupAsync()));



        private ICommand _forgetPasswordCommand;

        public ICommand ForgetPasswordCommand =>
            _forgetPasswordCommand ?? (_forgetPasswordCommand = new Command(async () =>
            {

                Device.OpenUri(new Uri("https://account.trandawl.cn/phonepassword/index"));
            }));
        async Task ExecuteSignupAsync()
        {


            Device.OpenUri(new Uri("https://account.trandawl.cn/Account/Register"));
            //  await NavigationService.PushModalAsync(this.Navigation, new RegisterView());
            //NavigationPage 
            // await Navigation.PushModalAsync(new SNavigationPage(new RegisterView()));
        }
        async Task ExecuteLoginAsync()
        {
            if (IsBusy)
                return;


            Message = "正在登录...";

            if (string.IsNullOrWhiteSpace(Email))
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
                {
                    Title = "登录信息",
                    Message = "登录名称不能为空",
                    Cancel = "确认"
                });
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
                {
                    Title = "登录信息",
                    Message = "登录密码不能为空",
                    Cancel = "确认"
                });
                return;
            }

            try
            {
                IsBusy = true;


                Message = "正在登录....";

                // await Task.Delay(2000);


                var authonservices = Xamarin.Forms.DependencyService.Get<IdentityService>();

                var accessresult = await authonservices.LoginAsync(Email, Password);

                if (accessresult.IsError == false)
                {
                    try
                    {



                        GlobalSetting.Instance.UserToken = accessresult.UserToken;

                      //  MessagingService.Current.SendMessage<>
                     await Navigation.PopModalAsync();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                }
                else
                {
                    MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message,
                        new MessagingServiceAlert
                        {
                            Title = "登录失败",
                            Message = accessresult.ErrorMsg,
                            Cancel = "确认"
                        });
                }

                IsBusy = false;
                // return Navigation.PushModalAsync(new RootPageAndroid(), true);
                // return  Navigation.PushAsync(new NavigationPage(new RootPageAndroid()));
                //  return UserDialogs.Instance.AlertAsync("密码错误", "消息", "确定");
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                Message = "登录";
                IsBusy = false;
            }


        }
    }
}
