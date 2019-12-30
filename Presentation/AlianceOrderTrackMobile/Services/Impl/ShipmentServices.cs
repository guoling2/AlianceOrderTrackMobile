using AlianceOrderTrackMobile.Model;
using AlianceOrderTrackMobile.Services.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using IdentityModel.Client;
using XamarinSharedLibrary.IdentityModel;

namespace AlianceOrderTrackMobile.Services.Impl
{
    public class ShipmentService : IShipmentServices
    {


        public IRequestProvider RequestProvider { get; private set; }

        public ShipmentService()
        {
            RequestProvider = Xamarin.Forms.DependencyService.Resolve<IRequestProvider>();
        }
      
        public async Task<LogisticTrackView> Query(string taskId, string logisticStoreId)
        {
           string searchurl = GlobalSetting.Instance.GatewayLogisticEndpoint + 
               $"/api/Shipment/querybytracknumber?TrackorderId={taskId}&LogisticStoreId={logisticStoreId}";
            var httpclient = RequestProvider.CreateHttpClient();

            var token =await httpclient.ReadSystemAccessToken();

            httpclient.SetBearerToken(token);

            var ppp = await httpclient.GetStringAsync(searchurl).ConfigureAwait(false);//.GetAwaiter().GetResult();

            return await Task.Run(() =>
                 JsonConvert.DeserializeObject<LogisticTrackView>(ppp));
        }


        public async Task<TmsResponse> ShipmentUpdateStatued(ShipmentStatuedChangedModel statuedChangedModel)
        {
            string searchurl = GlobalSetting.Instance.GatewayLogisticEndpoint +
                               $"/api/ShipmentTransport/updatestatued";

         

           var result =
               await RequestProvider.PostAsyncTmsResponse<ShipmentStatuedChangedModel>(searchurl,
                   statuedChangedModel);


           return result;


        }
    }
}
