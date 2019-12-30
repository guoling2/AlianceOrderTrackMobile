using System;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Model;
using AlianceOrderTrackMobile.Services.Abstractions;
using XamarinSharedLibrary.IdentityModel;

namespace AlianceOrderTrackMobile.Services.Impl
{
   public class ShipmentPlanServices: IShipmentPlanServices
    {

        public IRequestProvider RequestProvider { get; private set; }

        public ShipmentPlanServices()
        {
            RequestProvider = Xamarin.Forms.DependencyService.Resolve<IRequestProvider>();
        }
        public async Task<TmsResponse> Shipmentcreateplan(ShipmentPlanRequest shipmentPlanRequest)
        {
            string searchurl = GlobalSetting.Instance.GatewayLogisticEndpoint +
                               $"/api/shipmentPlan/open";

            var result =
                await RequestProvider.PostAsyncTmsResponse<ShipmentPlanRequest>(searchurl,
                    shipmentPlanRequest);


            return result;
        }
    }
}
