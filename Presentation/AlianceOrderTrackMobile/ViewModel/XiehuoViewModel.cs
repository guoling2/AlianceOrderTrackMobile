using AlianceOrderTrackMobile.Model;
using FormsToolkit;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using XamarinSharedLibrary.Help;
using XamarinSharedLibrary.Model;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Model.Xieche;
using AlianceOrderTrackMobile.Services.Abstractions;
using AlianceOrderTrackMobile.Services.Impl;
using AlianceOrderTrackMobile.ViewModel.Model;
using Dasync.Collections;
using XamarinSharedLibrary.Services;

namespace AlianceOrderTrackMobile.ViewModel
{
    public class XiehuoViewModel : ViewModelBase
    {

        private ZXingScannerServices zXingScannerServices;

        private ObservableCollection<LogisticStore> _userAuthorizeStores;
        public ObservableCollection<ScanOrderModel> OrderCollections { get; set; }

        public ObservableCollection<LogisticStore> UserAuthorizeStores
        {

            get
            {

               
                return _userAuthorizeStores;
            }
            set
            {
                _userAuthorizeStores = value;
                RaisePropertyChanged(() => UserAuthorizeStores);
            }


        }

        private LogisticStore _selectedLogisticStore;

        public LogisticStore SelectedLogisticStore
        {

            get => _selectedLogisticStore;
            set
            {
                _selectedLogisticStore = value;
                RaisePropertyChanged(() => SelectedLogisticStore);
            }
        }

        public string SelectXieModel { get; set; } = "整单卸货";

        public  async Task  InitializeMyStoreList()
        {


            if (_userAuthorizeStores.Count > 0)
            {
                return;
            }
            IsBusy = true;
           
            var loadstores = await logisticStoreServices.GetItemsAsync();

            if (loadstores.Count != 0)
            {

                loadstores.ForEach(a =>
                {
                    _userAuthorizeStores.Add(a);

                });

                SelectedLogisticStore = _userAuthorizeStores[0];

            }

            IsBusy = false;
        }

        private ILogisticStoreServices logisticStoreServices;


        IShipmentXiecheServices _shipmentServices;
        public XiehuoViewModel(INavigation navigation) : base(navigation)
        {


            logisticStoreServices = Xamarin.Forms.DependencyService.Resolve<ILogisticStoreServices>();


            _shipmentServices= Xamarin.Forms.DependencyService.Resolve<IShipmentXiecheServices>();

            zXingScannerServices = new ZXingScannerServices(navigation)
            {
                CallBackAction = async (a) => await SearchOrderAsync(a.ToString())
            };

            _userAuthorizeStores = new ObservableCollection<LogisticStore>();

            OrderCollections = new ObservableCollection<ScanOrderModel>();

            SearchOrderCommand = new Command(async (trackorderId) =>
            {
                if (trackorderId != null)
                {
                    await SearchOrderAsync(trackorderId.ToString());
                }
            });

            RemoveItemCommand = new Command((trackorderId) => RemoveOrder(trackorderId.ToString()));

            QrScanCommand = new Command(async (a) => await zXingScannerServices.OpenQrPage());

            SaveOrdersCommand = new Command(async (a) => await SaveOrders());


            IsBusy = true;
        }

        private async Task SaveOrders()
        {
            if (OrderCollections.Count == 0)
            {
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync("没有需要卸货的订单");
            }

            IsBusy = true;

            await OrderCollections.AsQueryable().ParallelForEachAsync(async a =>
            {
                a.ServerStatued = "上传中...";

               var result= await _shipmentServices.XieChe(new XiecheRequestModel()
                {
                    ActionStoreId = _selectedLogisticStore.StoreId,
                     RealOrderCount=a.OrderCount,
                      XieCheId=a.ServerId
               });
               if (result.HasError)
               {
                   a.ServerStatued = result.Error.ErrorMsg;

                }
               else
               {
                   RemoveOrder(a.TaskId);
               }
               // Thread.Sleep(1000);
             //   a.ServerStatued = "传输结束";

            });

            await Acr.UserDialogs.UserDialogs.Instance.AlertAsync("卸货完成！");

            IsBusy = false;
        }

        private void RemoveOrder(string p)
        {

          var taskorder=  OrderCollections.FirstOrDefault(t => t.TaskId == p);
            if (taskorder != null)
            {
                OrderCollections.Remove(taskorder);
            }
           
        }

     
        public Command SearchOrderCommand { get; set; }

        public Command RemoveItemCommand { get; set; }

        public Command SaveOrdersCommand { get; set; }
        private async Task SearchOrderAsync(string p)
        {

            if (string.IsNullOrWhiteSpace(p))
            {
                return;
            }

            if (IsBusy == true)
            {
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync("系统正在检索，请稍后。");

                return;
            }

            //OrderCollections.Add(new ScanOrderModel()
            //{

            //    TaskId = p,
            //    ServerId = p,
            //    ServerStatued = "1",
            //    ToCity = p
            //});

            //return;

            IsBusy = true;

           // var IsStop = false;

           string xxx = SelectXieModel;

           TmsResponseEvolution<XiecheScanResult> resulta = null;

           if (SelectXieModel == "整单卸货")
           {
               resulta = await _shipmentServices.Query(p, _selectedLogisticStore.StoreId);
            }
           

            if (resulta == null)
            {
                IsBusy = false;
                return;
            }

            if (resulta.HasError)
            {
                IsBusy = false;
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(p+"没有对应的卸货任务");
                return;
            }

            if (resulta.StatusCode == (int)ShipmentStatus.Xiehuoed)
            {
                IsBusy = false;
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(resulta.Data.XieCheCode + "已卸货");
                return;
            }

            if (OrderCollections.Any(t => t.ServerId == resulta.Data.XieCheId))
            {
                IsBusy = false;
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(resulta.Data.XieCheCode + "已经添加");
                return;
                // return;
            }

            OrderCollections.Add(new ScanOrderModel()
            {

                TaskId = resulta.Data.XieCheCode,
                ServerId = resulta.Data.XieCheId,
                ServerStatued ="",
                OrderCount = resulta.Data.OrderCount
            });

            IsBusy = false;
        }


       
     
    }


   
}