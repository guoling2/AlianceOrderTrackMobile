using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace AlianceOrderTrackMobile.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();

           // MPage.Resources.Add（StyleSheet.FromAssemblyResource（typeof（MasterPage）.Assembly，”Test.Styles.Menu.css“））;
            //AlianceOrderTrackMobile.Styles.
           //this.Resources.Add(new StyleSheet()
             var LoginPageSheet =   StyleSheet.FromAssemblyResource(typeof(LoginPage).Assembly, "AlianceOrderTrackMobile.Styles.LoginPage.css");
             this.Resources.Add(LoginPageSheet);

        }
	}
}