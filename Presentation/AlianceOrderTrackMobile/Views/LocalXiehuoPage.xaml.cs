﻿using Acr.UserDialogs;
using AlianceOrderTrackMobile.ViewModel;
using FormsToolkit;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsBuinessCommonLibrary.Help;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSharedLibrary.Broadcast;
using XamarinSharedLibrary.Services;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace AlianceOrderTrackMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocalXiehuoPage : ContentPage, IXamarinPageInitialize
    {

        private IBroadcastService broadcastService;
        XiehuoViewModel xiehuoViewModel;
        public LocalXiehuoPage ()
		{
			InitializeComponent ();

            xiehuoViewModel = new XiehuoViewModel(this.Navigation);

            this.BindingContext = xiehuoViewModel;

            //注册扫描
            broadcastService = BroadcastHelp.CreateBarcode1Service();

            broadcastService.Result += BroadcastService_Result;

        }
        private async void BroadcastService_Result(object sender, BroadcastReceiveEventArgs e)
        {
            xiehuoViewModel.SearchOrderCommand.Execute(e.Result);


        }
        protected override void OnAppearing()
        {
            broadcastService.SetBroadcast();
        

         

            xiehuoViewModel.InitializeComplateAction = async () =>
            {

                await xiehuoViewModel.InitializeMyStoreList();
                //  await NavigationService.PushAsync(Navigation, new SNavigationPage(new BatchSignView()));
            };

            xiehuoViewModel.InitializeComplateAction.Invoke();

            this.HandSearchOrder.Focus();

            base.OnAppearing();
        }
        public  Task Initialize()
        {


            return Task.CompletedTask;


             //return Task.CompletedTask;
        }

        protected override void OnDisappearing()
        {
            broadcastService.UnregisterReceiver();

            base.OnDisappearing();
        }

        private void HandSearchOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                return;
            }
            if (e.NewTextValue.Contains("\n"))
            {
                xiehuoViewModel.SearchOrderCommand.Execute(e.OldTextValue);

               // this.HandSearchOrder.Text = "";

                //this.HandSearchOrder.Focus();
            }
           
        }
    }

 
}