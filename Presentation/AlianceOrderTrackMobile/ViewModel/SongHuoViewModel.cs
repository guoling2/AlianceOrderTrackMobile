using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AlianceOrderTrackMobile.Model;
using AlianceOrderTrackMobile.Services.Abstractions;
using AlianceOrderTrackMobile.ViewModel.Model;
using Xamarin.Forms;
using XamarinSharedLibrary.Services;

namespace AlianceOrderTrackMobile.ViewModel
{
    public class SongHuoViewModel : XamarinSharedLibrary.Model.ViewModelBase
    {
        private ObservableCollection<LogisticStore> _userAuthorizeStores;

        private LogisticStore _selectedLogisticStore;

        private ILogisticStoreServices logisticStoreServices;

        IShipmentServices _shipmentServices;

        private IShipmentPlanServices shipmentPlanServices;
        private ZXingScannerServices zXingScannerServices;

        public SongHuoViewModel(INavigation navigation) : base(navigation)
        {

            logisticStoreServices = Xamarin.Forms.DependencyService.Resolve<ILogisticStoreServices>();


            _shipmentServices = Xamarin.Forms.DependencyService.Resolve<IShipmentServices>();

            shipmentPlanServices = Xamarin.Forms.DependencyService.Resolve<IShipmentPlanServices>();

            zXingScannerServices = new ZXingScannerServices(navigation)
            {
                CallBackAction = async (a) => await SearchOrderAsync(a.ToString())
            };


            SearchOrderCommand = new Command(async (trackorderId) => await SearchOrderAsync(trackorderId.ToString()));


            QrScanCommand = new Command(async (a) => await zXingScannerServices.OpenQrPage());

            RemoveItemCommand = new Command((trackorderId) => RemoveOrder(trackorderId.ToString()));

            CreatePlanCommand = new Command(async() =>await CreatePlan());

            _userAuthorizeStores = new ObservableCollection<LogisticStore>();

            OrderCollections = new ObservableCollection<ScanOrderModel>();

          

        }
        private void RemoveOrder(string p)
        {

            var taskorder = OrderCollections.SingleOrDefault(t => t.TaskId == p);
            if (taskorder != null)
            {
                OrderCollections.Remove(taskorder);
            }

        }



        /// <summary>
        /// 创建排车计划
        /// </summary>
        private async Task CreatePlan()
        {


            IsBusy = true;

            var IsStop = false;

            if (OrderCollections.Count == 0)
            {
                IsStop = true;
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync("没有可以安排的订单");
            }

            if (SelectDriver == null)
            {
                IsStop = true;
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync("没有可以委派的司机");
            }

            try
            {
                if (IsStop)
                {
                    return;
                }
                var result = await shipmentPlanServices.Shipmentcreateplan(new ShipmentPlanRequest()
                {
                    TaskType = ShipmentPlanTaskType.SendItem,
                    ShipmentUserId = SelectDriver.UserId,
                    FullShipmentId = OrderCollections.Select(t => t.ServerId).ToArray()
                });
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(new AlertConfig()
                {
                    Message = result.Info,

                });

                if (result.StatusCode == (int)TmsStatusCodeEnum.Success)
                {
                    OrderCollections.Clear();
                }
            }
            catch (Exception e)
            {
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(new AlertConfig()
                {
                    Message = "网络错误",

                });
            }

            IsBusy = false;

        }

        public Command CreatePlanCommand { get; set; }
        public Command RemoveItemCommand { get; set; }
        private async Task SearchOrderAsync(string p)
        {


            IsBusy = true;

            var IsStop = false;
            var resulta = await _shipmentServices.Query(p, _selectedLogisticStore.StoreId);

            if (resulta == null)
            {
                IsStop = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(resulta.ShipmentId))
            {
                IsStop = true;
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync("没有数据");
            }

            if (resulta.ShipmentStatuedId == (int)ShipmentStatus.FullShipped)
            {
                IsStop = true;
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(resulta.TrackOrderId + "已安排配送");
            }

            if (OrderCollections.Any(t => t.ServerId == resulta.ShipmentId))
            {
                IsStop = true;
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(resulta.TrackOrderId + "已经添加");
                // return;
            }

            if (IsStop == false)
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
        public Command SearchOrderCommand { get; set; }

        public async Task InitializeMyStoreList()
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

        public LogisticStore SelectedLogisticStore
        {

            get => _selectedLogisticStore;
            set
            {
                _selectedLogisticStore = value;
                RaisePropertyChanged(() => SelectedLogisticStore);
            }
        }

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

        private PackgeDriverDetail _selectDriver;
        public PackgeDriverDetail SelectDriver
        {
            get => _selectDriver;
            set
            {
                _selectDriver = value;
                RaisePropertyChanged(() => SelectDriver);
            }
        }
    }
}
