using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FormsToolkit;
using Newtonsoft.Json;
using XamarinSharedLibrary.Help;
using XamarinSharedLibrary.IdentityModel;
using XamarinSharedLibrary.Model.Token;

namespace AlianceOrderTrackMobile.Services
{
    public abstract class BaseWebApiServices<T> : IDataStore<T>
    {
     

        public IRequestProvider RequestProvider { get;private set; }

        public BaseWebApiServices()
        {
           
            RequestProvider=Xamarin.Forms.DependencyService.Resolve<IRequestProvider>();
        }

        public string RunUrl { get; set; }

        public virtual Task<bool> AddItemAsync(T item)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> DeleteItemAsync(T item)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }


        public virtual async Task<List<T>> GetItemsAsync(SystemSearchParameter systemSearch)
        {


          var accesstoken=await RequestProvider.CreateHttpClient().ReadSystemAccessToken();

          return await RequestProvider.GetAsync<List<T>>(RunUrl, systemSearch, accesstoken);
          // return  await Task.Run< List<T>>(() => JsonConvert.DeserializeObject<List<T>>(readresult)); ;
        }
       

        public virtual Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> PullLatestAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> SyncAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> UpdateItemAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}
