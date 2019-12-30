using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSharedLibrary.Model.Token;

namespace AlianceOrderTrackMobile.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginDaniaPage : ContentPage
	{
		public LoginDaniaPage ()
		{
			InitializeComponent ();


            var xxx= new LoginViewModel(this.Navigation);
		  
            this.BindingContext = xxx;
        }
    }
}