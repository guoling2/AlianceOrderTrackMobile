using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Model;
using AlianceOrderTrackMobile.Services.Abstractions;
using IdentityModel.Client;
using Newtonsoft.Json;
using XamarinSharedLibrary.IdentityModel;

namespace AlianceOrderTrackMobile.Services.Impl
{
    public class LogisticStoreServices: BaseWebApiServices<LogisticStore>, ILogisticStoreServices
    {
        public Task<bool> AddItemAsync(LogisticStore item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(LogisticStore item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(LogisticStore item)
        {
            throw new NotImplementedException();
        }

        public Task<LogisticStore> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<LogisticStore>> GetItemsAsync(SystemSearchParameter systemSearch = null)
        {
            this.RunUrl = GlobalSetting.Instance.GatewayLogisticEndpoint + "/api/LogisticStoreAuthorize/MyStores";

            try
            {
       
                var httpclient = RequestProvider.CreateHttpClient();

                var token=await httpclient.ReadSystemAccessToken();

                httpclient=RequestProvider.CreateHttpClient(token);

                var searchurl = new UrlBuilder().Build(RunUrl, systemSearch);

                var searchresult = await httpclient.GetStringAsync(searchurl).ConfigureAwait(false);

                return    await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<LogisticStore>>(searchresult));

                //  return base.GetItemsAsync(null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }    
           
        }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PullLatestAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SyncAsync()
        {
            throw new NotImplementedException();
        }
    }
}
