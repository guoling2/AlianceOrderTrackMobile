using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinSharedLibrary.Model
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        // protected readonly IDialogService DialogService;
        //  protected readonly INavigationService NavigationService;


        public ICommand NavigateCommand { private set; get; }

        public INavigation Navigation { get; }

        //public ViewModelBase()
        //{
           
        //}
        protected ViewModelBase(INavigation navigation)
        {
            Navigation = navigation;

            Message = "正在处理...";

            NavigateCommand = new Command<Type>(
                async (Type pageType) =>
                {
                    Xamarin.Forms.Page page = (Xamarin.Forms.Page)Activator.CreateInstance(pageType);
                    await Navigation.PushAsync(page);
                });
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }


        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                RaisePropertyChanged(() => Message);
            }
        }

        protected ViewModelBase()
        {
            //DialogService = ViewModelLocator.Resolve<IDialogService>();
           // NavigationService = ViewModelLocator.Resolve<INavigationService>();
           // GlobalSetting.Instance.BaseEndpoint = Settings.UrlBase;
        }


        public Action InitializeComplateAction { get; set; }

        public virtual List<Action> ComplateAction { get; set; }


        public Command CallPhoneCommand { get; set; }

        public Command QrScanCommand { get; set; }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}
