using AlianceOrderTrackMobile.Page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.ViewModel;
using FormsToolkit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSharedLibrary.Help;
using XamarinSharedLibrary.IdentityModel;

namespace AlianceOrderTrackMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyDashbordExHead : ContentPage
    {
        public ListView ListView;

        public MyDashbordExHead()
        {
            InitializeComponent();

            var x= new MyDashbordExMasterViewModel();
          

            if (DefaultPage == null)
            {

                DefaultPage = new LocalXiehuoPage();
            }

            BindingContext = x;
            ListView = MenuItemsListView;
        }

        public Xamarin.Forms.Page DefaultPage { get; set; }
        class MyDashbordExMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MyDashbordExMenuItem> MenuItems { get; set; }
            
            public MyDashbordExMasterViewModel()
            {
                MenuItems = new ObservableCollection<MyDashbordExMenuItem>(new[]
                {
                   // new MyDashbordExMenuItem { Id = 0, Title = "客户订单",TargetTypeString="AlianceOrderTrackMobile.Views.CustomerOrderPage" } ,
                    new MyDashbordExMenuItem { Id = 0, Title = "场站卸货",TargetTypeString="AlianceOrderTrackMobile.Views.LocalXiehuoPage" } ,
                 //   new MyDashbordExMenuItem { Id = 2, Title = "配载送货",TargetTypeString="AlianceOrderTrackMobile.Views.SonghuoPage" },
                    new MyDashbordExMenuItem { Id = 1, Title = "扫描设置",TargetTypeString="AlianceOrderTrackMobile.Views.Setting.BroadcastConfigPage" }


                   
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }



        public Func<SelectedItemChangedEventArgs, Xamarin.Forms.Page> SelectAction { get; set; }
        private async void MenuItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           var page= SelectAction(e);

           var inx = page as IXamarinPageInitialize;

           if (inx != null)
           {
               await inx.Initialize();
           }
        }
    }
}