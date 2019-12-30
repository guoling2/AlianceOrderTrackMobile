using AlianceOrderTrackMobile.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Model;
using AlianceOrderTrackMobile.ViewModel.Model;
using FormsToolkit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSharedLibrary.Help;

namespace AlianceOrderTrackMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilterDriverPage : ContentPage
	{

        // SearchDriverCommand
	    private FilterDriverViewModel filterDriverViewModel;
        public FilterDriverPage ()
		{
			InitializeComponent ();

            this.BindingContext = filterDriverViewModel=new FilterDriverViewModel(this.Navigation);

            ToolbarDone.Command = new Command(async () =>
            {



                await Navigation.PopModalAsync();

                if (Device.RuntimePlatform == Device.Android)
                {
                    MessagingService.Current.SendMessage<PackgeDriverDetail>(MessageKeys.SelectDriverEvent,
                        filterDriverViewModel.SelectDriver);
                }


            });

        }


	    public FilterDriverPage(string drivername) : this()
	    {
	        Device.BeginInvokeOnMainThread(action: async () =>
	        {

                filterDriverViewModel.SearchDriverCommand.Execute(drivername);

            });

           

	    }
    }

   
}