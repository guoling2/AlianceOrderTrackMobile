using AlianceOrderTrackMobile.Model;
using FormsToolkit;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XamarinSharedLibrary.Help;
using XamarinSharedLibrary.Model;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Services.Abstractions;
using AlianceOrderTrackMobile.Services.Impl;
using AlianceOrderTrackMobile.ViewModel.Model;
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


        IShipmentServices _shipmentServices;
        public XiehuoViewModel(INavigation navigation) : base(navigation)
        {


            logisticStoreServices = Xamarin.Forms.DependencyService.Resolve<ILogisticStoreServices>();


            _shipmentServices= Xamarin.Forms.DependencyService.Resolve<IShipmentServices>();

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
        }

        private async Task SaveOrders()
        {
            if (OrderCollections.Count == 0)
            {
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync("没有需要卸货的订单");
            }

            IsBusy = true;

            foreach (var a in OrderCollections)
            {
                var result = await _shipmentServices.ShipmentUpdateStatued(new ShipmentStatuedChangedModel()
                {
                    CommandId = (int)ShipmentStatus.Tihuoed,
                    ShipmentId = a.ServerId,

                });

                if (result.StatusCode == (int)TmsStatusCodeEnum.Success)
                {
                    a.ServerStatued = "成功";
                }
                else
                {
                    a.ServerStatued = result.Info;
                }
               
            }
            IsBusy = false;
        }

        private void RemoveOrder(string p)
        {

          var taskorder=  OrderCollections.SingleOrDefault(t => t.TaskId == p);
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


            OrderCollections.Add(new ScanOrderModel()
            {

                TaskId = p,
                ServerId = p,
                ServerStatued = "1",
                ToCity = p
            });

            return;

            IsBusy = true;

            var IsStop = false;
           var resulta=await _shipmentServices.Query(p, _selectedLogisticStore.StoreId);

            if (resulta == null)
            {
                IsStop = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(resulta.ShipmentId))
            {
                IsStop = true;
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(p+"没有对应的卸货任务");
            }

            if (resulta.ShipmentStatuedId == (int)ShipmentStatus.Xiehuoed)
            {
                IsStop = true;
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(resulta.TrackOrderId + "已卸货");
            }

            if (OrderCollections.Any(t => t.ServerId == resulta.ShipmentId))
            {
                IsStop = true;
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(resulta.TrackOrderId+"已经添加");
               // return;
            }
           
            if(IsStop==false)
            {
                OrderCollections.Add(new ScanOrderModel()
                {

                    TaskId = resulta.TrackOrderId,
                    ServerId = resulta.ShipmentId,
                    ServerStatued = resulta.ReceivedTotalCount.ToString(),
                    ToCity = resulta.DestCity
                });
            }
           
            IsBusy = false;
        }


       
     
    }


   
}