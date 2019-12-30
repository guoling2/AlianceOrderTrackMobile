using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinSharedLibrary.Controls
{
    public class SNavigationPage : NavigationPage
    {
        public SNavigationPage(Page root) : base(root)
        {
            Init();
            Title = root.Title;
            Icon = root.Icon;
        }

        public SNavigationPage()
        {
            Init();
        }

        void Init()
        {


            if (Device.RuntimePlatform == Device.iOS)
            {
                BarBackgroundColor = Color.FromHex("FAFAFA");
            }
            else
            {
                BarBackgroundColor = (Color)Application.Current.Resources["BackgroundColor"];
                BarTextColor = (Color)Application.Current.Resources["NavigationText"];
            }
        }
    }
}
