using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Model;
using AlianceOrderTrackMobile.Model.Xieche;
using AlianceOrderTrackMobile.Services.Abstractions;
using IdentityModel.Client;
using Newtonsoft.Json;
using XamarinSharedLibrary.IdentityModel;

namespace AlianceOrderTrackMobile.Services.Impl
{
    public class ShipmentXiecheServices: IShipmentXiecheServices
    {
        public IRequestProvider RequestProvider { get; private set; }

        public ShipmentXiecheServices()
        {
            RequestProvider = Xamarin.Forms.DependencyService.Resolve<IRequestProvider>();
        }


        public async Task<TmsResponseEvolution<XiecheScanResult>> Query(string taskId, string logisticStoreId)
        {
            string searchurl = GlobalSetting.Instance.GatewayLogisticEndpoint +
                               $"/api/ShipmentXieche/Scan?XieCheCode={taskId}&ActionStoreId={logisticStoreId}";
            var httpclient = RequestProvider.CreateHttpClient();

            var token = await httpclient.ReadSystemAccessToken();

            httpclient.SetBearerToken(token);

            try
            {
                var ppp =await httpclient.GetStringAsync(searchurl);

                var httpresult = JsonConvert.DeserializeObject<TmsResponseEvolution<XiecheScanResult>>(ppp);

                return httpresult;
                //return await Task.Run(() =>
                //    JsonConvert.DeserializeObject<TmsResponseEvolution<XiecheScanResult>>(ppp));
            }
            catch (Exception e)
            {
                return new TmsResponseEvolution<XiecheScanResult>()
                {
                    Error = new TmsResponseError("检索数据错误", "0")
                };

            }

           
        }

        public async Task<TmsResponse> XieChe(XiecheRequestModel xiecheRequest)
        {
            string searchurl = GlobalSetting.Instance.GatewayLogisticEndpoint +
                               $"/api/ShipmentXieche/XieChe";

            var result =
                await RequestProvider.PostAsyncTmsResponse<XiecheRequestModel>(searchurl,
                    xiecheRequest);

            return result;
        }
    }
}
