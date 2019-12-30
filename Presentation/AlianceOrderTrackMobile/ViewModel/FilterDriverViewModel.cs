using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AlianceOrderTrackMobile.Model;
using AlianceOrderTrackMobile.Services.Abstractions;
using AlianceOrderTrackMobile.ViewModel.Model;

namespace AlianceOrderTrackMobile.ViewModel
{
    public class FilterDriverViewModel : XamarinSharedLibrary.Model.ViewModelBase
    {
        public ObservableCollection<PackgeDriverDetail> PackgeDriverCollections { get; set; }


        private IDriverUserServices driverUserServices;

        public FilterDriverViewModel(INavigation navigation):base(navigation)
        {

            SearchDriverCommand = new Command(async (a) => { await SearchDriver(a); });

            PackgeDriverCollections = new ObservableCollection<PackgeDriverDetail>();


            driverUserServices = Xamarin.Forms.DependencyService.Resolve<IDriverUserServices>();

        }


        public PackgeDriverDetail SelectDriver { get; set; }
        public Command SearchDriverCommand { get; set; }


        private async Task SearchDriver(object content)
        {

            try
            {
                IsBusy = true;

                PackgeDriverCollections.Clear();


                string drivername=null,drivertel = null;

                if (content != null)
                {
                    if (content.ToString().Length == 11)
                    {
                        drivertel = content.ToString();
                    }
                    else
                    {
                        drivername = content.ToString();
                    }
                }

                var driverprofileresult = await driverUserServices.GetItemsAsync(drivername, drivertel);

                if (driverprofileresult.Count != 0)
                {
                    foreach (var driverUserProfile in driverprofileresult)
                    {
                        PackgeDriverCollections.Add(new PackgeDriverDetail()
                        {
                            //CarNumber = "鲁F1",
                            MobileNumber = driverUserProfile.Tel,
                            RealName = driverUserProfile.RealName,
                            ServerStatuedDesc = "正常",
                            UserId = driverUserProfile.UserId
                        });
                    }
                   
                }

                IsBusy = false;

            }
            catch (Exception)
            {

                throw;
            }
         
        }
    }

 
}
