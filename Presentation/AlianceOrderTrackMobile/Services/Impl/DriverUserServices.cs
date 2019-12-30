using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Model;
using AlianceOrderTrackMobile.Services.Abstractions;
using IdentityModel.Client;
using Newtonsoft.Json;
using XamarinSharedLibrary.IdentityModel;

namespace AlianceOrderTrackMobile.Services.Impl
{
    public class DriverUserServices: IDriverUserServices
    {

        public IRequestProvider RequestProvider { get; private set; }

        public DriverUserServices()
        {
            RequestProvider = Xamarin.Forms.DependencyService.Resolve<IRequestProvider>();
        }
        public async Task<List<DriverUserProfile>> GetItemsAsync(string drivername, string drivertel)
        {
          var searchurl =  GlobalSetting.Instance.GatewayLogisticEndpoint + "/api/MyDriver/list";

          var httpclient = RequestProvider.CreateHttpClient();

          var token = await httpclient.ReadSystemAccessToken();

          httpclient.SetBearerToken(token);

          searchurl = new UrlBuilder().Build(searchurl, new {drivername=drivername,drivertel=drivertel });

          var ppp = await httpclient.GetStringAsync(searchurl).ConfigureAwait(false);//.GetAwaiter().GetResult();

          return await Task.Run(() =>
              JsonConvert.DeserializeObject<List<DriverUserProfile>>(ppp));
        }
    }
}
