using AlianceOrderTrackMobile.ViewModel;
using AlianceOrderTrackMobile.ViewModel.Model;
using FormsToolkit;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSharedLibrary.Controls;
using XamarinSharedLibrary.Help;

namespace AlianceOrderTrackMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SonghuoPage : ContentPage
    {
        private readonly SongHuoViewModel _songHuoViewModel;
        public SonghuoPage()
        {



            InitializeComponent();





            this.BindingContext = _songHuoViewModel = new SongHuoViewModel(this.Navigation);

            _songHuoViewModel.InitializeComplateAction = async () =>
            {

                await _songHuoViewModel.InitializeMyStoreList();

            };

            _songHuoViewModel.InitializeComplateAction.Invoke();

            MessagingService.Current.Subscribe<PackgeDriverDetail>(MessageKeys.SelectDriverEvent,
                (a, b) =>
                {
                    _songHuoViewModel.SelectDriver = b;
                });
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {


            await NavigationService.PushModalAsync(
                Navigation,

                userdriverdt.Text.Length == 0 ? new SNavigationPage(new FilterDriverPage()) :
                new SNavigationPage(new FilterDriverPage(userdriverdt.Text))

                );

        }
    }
}