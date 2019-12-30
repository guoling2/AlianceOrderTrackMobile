using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlianceOrderTrackMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageListPage : ContentPage
	{
		public PageListPage ()
		{
			InitializeComponent ();


            NavigateCommand = new Command<Type>(
		        async (Type pageType) =>
		        {
		            Xamarin.Forms.Page page = (Xamarin.Forms.Page)Activator.CreateInstance(pageType);
		            await Navigation.PushAsync(page);
		        });

		    BindingContext = this;
        }
	    public ICommand NavigateCommand { private set; get; }

        private void Button_Clicked(object sender, EventArgs e)
        {

            try
            {
                PhoneDialer.Open("15865638660");
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}