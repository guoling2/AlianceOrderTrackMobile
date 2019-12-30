using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FormsToolkit;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
namespace XamarinSharedLibrary.Services
{
    public class ZXingScannerServices
    {


        private INavigation navigation;

        public ZXingScannerServices(INavigation navigation)
        {
            this.navigation = navigation;



            InitScanPage();

        }

        public Lazy<ZXingScannerPage> ZXingScannerPage
        {
            get;
            protected set;
        }

        public Action<ZXing.Result> CallBackAction { get; set; }

        void InitScanPage()
        {

            ZXingScannerPage = new Lazy<ZXingScannerPage>(() =>
            {
                var page = new ZXingScannerPage(new MobileBarcodeScanningOptions
                {
                    AutoRotate = true,
                    PossibleFormats = new List<ZXing.BarcodeFormat>()
                    {
                        ZXing.BarcodeFormat.QR_CODE,
                        ZXing.BarcodeFormat.CODE_128,
                        ZXing.BarcodeFormat.CODE_39,
                        ZXing.BarcodeFormat.CODE_93,
                        ZXing.BarcodeFormat.EAN_13,
                        ZXing.BarcodeFormat.EAN_8
                    }
                })
                {
                    DefaultOverlayTopText = "对准屏幕的中的扫描框",
                    DefaultOverlayBottomText = "扫描",


                };



                page.OnScanResult += ScanPage_OnScanResult;


                page.Title = "单号扫描";

                var item = new ToolbarItem
                {
                    Text = "取消",
                    Command = new Command(async () =>
                    {
                        page.IsScanning = false;
                        await navigation.PopAsync();
                    })
                };

                if (Device.RuntimePlatform != Device.iOS)
                    item.Icon = "toolbar_close.png";

                page.ToolbarItems.Add(item);


                return page;
            }, true);
        }

        void ScanPage_OnScanResult(ZXing.Result result)

        {



            //var scanPage = ZXingScannerPage.Value;
            ZXingScannerPage.Value.IsScanning = false;

            DependencyService.Get<IAudioPlayerService>().Play("bibi.mp3");
            // await CrossMediaManager.Current.Play("http://www.montemagno.com/sample.mp3");

            // Stop scanning

            // Pop the page and show the result
            Device.BeginInvokeOnMainThread(async () =>
            {

                await navigation.PopAsync();

                CallBackAction?.Invoke(result);

            });
        }


        public async Task OpenQrPage()
        {

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {

                        await Acr.UserDialogs.UserDialogs.Instance.AlertAsync("提示", "需要开启权限", "确定");

                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Camera))
                        status = results[Permission.Camera];
                }

                if (status == PermissionStatus.Granted)
                {

                    await navigation.PushAsync(ZXingScannerPage.Value);
                }
                else if (status != PermissionStatus.Unknown)
                {

                    await Acr.UserDialogs.UserDialogs.Instance.AlertAsync("授权失败", "不能继续操作，请重试", "确定");
                }
            }
            catch (Exception ex)
            {


                MessagingService.Current.SendMessage("系统错误");
            }

        }
    }
}
