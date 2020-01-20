using FormsToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSharedLibrary.Help;
using XamarinSharedLibrary.IdentityModel;

namespace AlianceOrderTrackMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyDashbordEx : MasterDetailPage
    {


        private Dictionary<string, Type> PageCache;
        public MyDashbordEx()
        {
            InitializeComponent();


            PageCache = new Dictionary<string, Type>();

            if (this.Detail == null)
            {
                //  this.Detail = MasterPage.DefaultPage;

                Detail = new NavigationPage(MasterPage.DefaultPage);
                IsPresented = false;
            }



            MasterPage.SelectAction = (a) =>
            {

                var item = a.SelectedItem as MyDashbordExMenuItem;

                if (item == null)
                    return null;

                if (CurrentPageTitle != null)
                {


                    if (item.Title == CurrentPageTitle)
                    {
                        return null;
                    }
                }


                var pageheadhash = item.Title;

                if (!PageCache.ContainsKey(pageheadhash))
                {
                    var xxx = Type.GetType(item.TargetTypeString);
                    PageCache.Add(pageheadhash, xxx);
                }


                CurrentPageTitle = item.Title;

                var page = (Xamarin.Forms.Page)Activator.CreateInstance(PageCache[pageheadhash]);

                page.Title = item.Title;


                Detail = new NavigationPage(page) { Title = item.Title };
                IsPresented = false;

                MasterPage.ListView.SelectedItem = null;


                return page;
               

            };

            //  MasterPage.ListView.ItemSelected += ListView_ItemSelected1;
        }

      

        private string CurrentPageTitle;
        private async Task ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var item = e.SelectedItem as MyDashbordExMenuItem;

            if (item == null)
                return;

            if (CurrentPageTitle != null)
            {

              
                if (item.Title == CurrentPageTitle)
                {
                    return;
                }
            }




            var pageheadhash = item.Title;

            if(!PageCache.ContainsKey(pageheadhash))
            {
                var xxx = Type.GetType(item.TargetTypeString);
                PageCache.Add(pageheadhash, xxx);
            }


            CurrentPageTitle = item.Title;

            var page = (Xamarin.Forms.Page)Activator.CreateInstance(PageCache[pageheadhash]);

            page.Title = item.Title;
                
          
            Detail = new NavigationPage(page) {Title = item.Title};
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;

            var inx = page as IXamarinPageInitialize;

            if (inx != null)
            {
               await inx.Initialize();
            }



            //return Task.CompletedTask;
        }

        protected override void OnAppearing()
        {



            ReloadUserToken();


           

            base.OnAppearing();

           
        }

        private void ReloadUserToken()
        {

            var usertoken = GlobalSetting.Instance.UserToken;

            if (usertoken == null)
            {
                MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            }
            //else
            //{
            //  var xxx=  usertoken.ExpTime;
            //    if(usertoken.IsExprie)
            //    {
            //        MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            //    }
            //    //if()
            //    //MessagingService.Current.SendMessage(Settings.Current.LastExpirestimeteTime > DateTime.UtcNow
            //    //    ? MessageKeys.LoggedIn
            //    //    : MessageKeys.NavigateLogin);
            //}
        }
    }
}