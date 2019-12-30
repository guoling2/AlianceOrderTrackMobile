using System;
using System.Net.Http;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Model;
using IdentityModel.Client;
using Newtonsoft.Json;
using XamarinSharedLibrary.IdentityModel;
using XamarinSharedLibrary.Model.Token;

namespace AlianceOrderTrackMobile.Services
{
    public interface IRequestProvider
    {


        Task<TmsResponse> PostAsyncTmsResponse<TPostRequest>(string uri, TPostRequest data);
        //Task<TResult> PostAsync<TResult>(string uri, object data, string token = "", string header = "");

        //Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret);

        //Task<TResult> PutAsync<TResult>(string uri, object data, string token = "", string header = "");

        //Task DeleteAsync(string uri, string token = "");

        Task<TResult> GetAsync<TResult>(string uri, string token = "");

        Task<TResult> GetAsync<TResult>(string uri, object data, string token = "");

        HttpClient CreateHttpClient(string token = "");
    }

   

    public static class RequestProviderExtensions
    {




        public static async Task<HttpClient> SetBearTokenAndGetIt(this HttpClient httpClient)
        {

            var token =await httpClient.ReadSystemAccessToken();

            httpClient.SetBearerToken(token);

            return httpClient;

        }



       public static async Task<string> ReadSystemAccessToken(this HttpClient httpClient)
        {
            var g = GlobalSetting.Instance;
            var usertoken = g.UserToken;

            if (usertoken == null)
            {
                return "";
            }

          
            if (usertoken.IsExprie)
            {
                var tokenresponse =await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest()
                {

                    RefreshToken = usertoken.RefreshToken,
                    ClientId = GlobalSetting.Instance.ClientId,
                    ClientSecret = GlobalSetting.Instance.ClientSecret,
                    Address = GlobalSetting.Instance.TokenEndpoint,


                });
                usertoken=g.UserToken = tokenresponse.CreateUserTokenInfo();
            }

           
            return usertoken.AccessToken;

        }


        public static async Task<Tuple<System.Net.HttpStatusCode, T>> PostHttpContentAsync<T>(this IRequestProvider p, string uri,string token, HttpContent hc)
        {

            var httpclient = p.CreateHttpClient(token: token);


           // var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = hc };



            var response = await httpclient.PostAsync(uri,hc);


            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var resultstring = await response.Content.ReadAsStringAsync();

                T result = await Task.Run(() =>
                    JsonConvert.DeserializeObject<T>(resultstring));


                return new Tuple<System.Net.HttpStatusCode, T>(response.StatusCode, result);




            }
            else
            {
                return new Tuple<System.Net.HttpStatusCode, T>(response.StatusCode, default(T));
            }

        }
    }
}
