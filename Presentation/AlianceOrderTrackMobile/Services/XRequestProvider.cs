using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AlianceOrderTrackMobile.Model;
using AlianceOrderTrackMobile.Services.Exceptions;
using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using XamarinSharedLibrary.IdentityModel;
using XamarinSharedLibrary.Model.Token;

namespace AlianceOrderTrackMobile.Services
{
    public class XRequestProvider : IRequestProvider
    {
        private UrlBuilder urlBuilder;

        public XRequestProvider()
        {
            urlBuilder = new UrlBuilder();
        }

      

        public HttpClient CreateHttpClient(string token = "")
        {
            
           var httpClient = new HttpClient();

           var ppc = httpClient.Timeout;


            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {

                httpClient.SetBearerToken(token);
            }
            return httpClient;
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(token:token);

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
            httpRequest.Headers.Accept.Clear();
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response;

            try
            {
                response = await httpClient.SendAsync(httpRequest, cancellationToken: default).ConfigureAwait(false);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
            //var response = httpClient.GetAsync(uri).ConfigureAwait(false).GetAwaiter().GetResult();

            await HandleResponse(response);

            return await response.Content.ReadAsAsync<TResult>();
        }

        public Task<TResult> GetAsync<TResult>(string uri, object data, string token = "")
        {
            string newurl = uri;

            if (data != null)
            {
                 newurl = urlBuilder.Build(uri, data);
            }
            

            return GetAsync<TResult>(newurl, token);
        }

        public async Task<TmsResponse> PostAsyncTmsResponse<TPostRequest>(string uri, TPostRequest data)
        {


            var httpclient = await CreateHttpClient().SetBearTokenAndGetIt();

            var response = await httpclient.PostAsJsonAsync<TPostRequest>(uri, data);

             await HandleResponse(response);

           try
           {
                 return await response.Content.ReadAsAsync<TmsResponse>();
           }
           catch (Exception e)
           {
               return new TmsResponse()
               {
                    Info=e.Message
               };

           }
          
        }

        async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception(content);
                }

                throw new HttpRequestException(content);
            }
        }
    }


    public class UrlBuilder
    {


        public  string Build(string searchurl, object searchmodel=null)
        {

            if (searchmodel == null)
            {
                return searchurl;
            }
           var searhkeyvalues= ObjectToDictionary(searchmodel);

           return GenerceSearchQuery(searchurl, searhkeyvalues);
        }


        private Dictionary<string, object> ObjectToDictionary(object model)
        {

            Dictionary<string, object> valuesDictionary = new Dictionary<string, object>();

            List<string> searchkey = new List<string>();
            var searchmodeltype = model.GetType();

            searchmodeltype.GetProperties().ToList().ForEach(x =>
            {
                searchkey.Add(x.Name);
                valuesDictionary.Add(x.Name, "");
            });

            foreach (var valuesDictionaryKey in searchkey)
            {

                // var xxx = searchmodeltype.GetProperties().First(x => x.Name == valuesDictionaryKey);

                var result = searchmodeltype.GetProperties().First(x => x.Name == valuesDictionaryKey)
                    .GetValue(model, null);

                if (result != null)
                {
                    valuesDictionary[valuesDictionaryKey] = result;
                }

            }

            return valuesDictionary;
        }

        private string GenerceSearchQuery(string baseurl, Dictionary<string, object> valuesDictionary)
        {
            string s1 = "";



            if (valuesDictionary.Keys.Count == 0)
            {
                return baseurl;
            }
            foreach (var o in valuesDictionary.Keys)
            {
                var value = valuesDictionary[o];

                if (!string.IsNullOrWhiteSpace(value.ToString()))
                {
                    s1 += $"{o}={value}&";
                }

            }

            s1 = s1.Remove(s1.Length - 1, 1);
            return $"{baseurl}?{s1}";

        }
    }
    //{
    //    private readonly JsonSerializerSettings _serializerSettings;

    //    public XRequestProvider()
    //    {
    //        _serializerSettings = new JsonSerializerSettings
    //        {
    //            ContractResolver = new CamelCasePropertyNamesContractResolver(),
    //            DateTimeZoneHandling = DateTimeZoneHandling.Local,
    //            NullValueHandling = NullValueHandling.Ignore
    //        };
    //        _serializerSettings.Converters.Add(new StringEnumConverter());
    //    }

    //    public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
    //    {


    //        try
    //        {
    //            HttpClient httpClient = CreateHttpClient(token);




    //            HttpResponseMessage response = null;
    //            try
    //            {


    //                response = await httpClient.GetAsync(uri);
    //            }
    //            catch (Exception e)
    //            {
    //                Debugger.Log(1, "1", e.Message);
    //                //  throw;
    //            }

    //            //   HttpResponseMessage response = await httpClient.GetAsync(uri);

    //            await HandleResponse(response);


    //            var searchresult = await response.Content.ReadAsStringAsync();

    //            //TResult result = await Task.Run(() =>
    //            //    JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
    //            //  var p =JsonConvert.DeserializeObject<TResult>(searchresult);
    //            return JsonConvert.DeserializeObject<TResult>(searchresult);
    //        }

    //        catch (ServiceAuthenticationException e)
    //        {


    //            Settings.Current.AuthIdToken = "";
    //            Settings.Current.AuthAccessToken = "";


    //            // return  null;
    //            //Console.WriteLine(e);
    //            throw e;
    //        }


    //        //TResult result = await Task.Run(() =>
    //        //    JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

    //        //return result;
    //    }

    //    public async Task<TResult> PostAsync<TResult>(string uri, object data, string token = "", string header = "")
    //    {
    //        HttpClient httpClient = CreateHttpClient(token);

    //        if (!string.IsNullOrEmpty(header))
    //        {
    //            AddHeaderParameter(httpClient, header);
    //        }
    //        //httpClient.GetAsync
    //        //var content = new StringContent(JsonConvert.SerializeObject(data));
    //        //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
    //        //HttpResponseMessage responsex =
    //        //     httpClient.PostAsync(uri, new FormUrlEncodedContent(CollectionFormsData(data))).Result;

    //        //HttpResponseMessage response = 
    //        //     httpClient.PostAsync(uri, new FormUrlEncodedContent(data.ConvertKeyValuePair()))
    //        //     .GetAwaiter().GetResult();


    //        // httpClient.SendAsync(new HttpRequestMessage(WebRequestMethods.Http.Get,""),)

    //        HttpResponseMessage response = null;
    //        if (data == null)
    //        {
    //            response =
    //                await httpClient.PostAsync(uri, null);
    //            ;
    //        }
    //        else
    //        {
    //            response =
    //                   await httpClient.PostAsync(uri, new FormUrlEncodedContent(data.ConvertKeyValuePair()))
    //               ;
    //        }


    //        await HandleResponse(response);

    //        string serialized = await response.Content.ReadAsStringAsync();
    //        // string serialized = await response.Content.ReadAsStringAsync();
    //        //var xxx= JsonConvert.DeserializeObject<TResult>(serialized);

    //        return await Task.Factory.StartNew<TResult>(() => JsonConvert.DeserializeObject<TResult>(serialized));
    //        //TResult result = await Task.Run(() =>
    //        //    JsonConvert.DeserializeObject<TResult>(serialized));

    //        //return result;
    //    }

    //    public async Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret)
    //    {
    //        HttpClient httpClient = CreateHttpClient(string.Empty);

    //        if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret))
    //        {
    //            AddBasicAuthenticationHeader(httpClient, clientId, clientSecret);
    //        }

    //        var content = new StringContent(data);
    //        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
    //        HttpResponseMessage response = await httpClient.PostAsync(uri, content);

    //        await HandleResponse(response);
    //        string serialized = await response.Content.ReadAsStringAsync();

    //        TResult result = await Task.Run(() =>
    //            JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

    //        return result;
    //    }

    //    public async Task<TResult> PutAsync<TResult>(string uri, object data, string token = "", string header = "")
    //    {
    //        HttpClient httpClient = CreateHttpClient(token);

    //        if (!string.IsNullOrEmpty(header))
    //        {
    //            AddHeaderParameter(httpClient, header);
    //        }

    //        // var content = new StringContent(JsonConvert.SerializeObject(data));
    //        //   content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
    //        //HttpResponseMessage response = 
    //        //    httpClient.PutAsync(uri, new FormUrlEncodedContent(data.ConvertKeyValuePair())).Result;

    //        //HttpResponseMessage response =
    //        //  await httpClient.PutAsync(uri, new FormUrlEncodedContent(data.ConvertKeyValuePair()));


    //        //await HandleResponse(response);
    //        //string serialized = await response.Content.ReadAsStringAsync();


    //        return await Task.Run<TResult>(() => JsonConvert.DeserializeObject<TResult>(serialized));
    //        //TResult result = await Task.Run(() =>
    //        //    JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

    //        //return result;
    //    }

    //    public async Task DeleteAsync(string uri, string token = "")
    //    {
    //        HttpClient httpClient = CreateHttpClient(token);
    //        await httpClient.DeleteAsync(uri);
    //    }

    //    public HttpClient CreateHttpClient(string token = "")
    //    {
    //        var httpClient = new HttpClient();

    //        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //        if (!string.IsNullOrEmpty(token))
    //        {

    //            httpClient.SetBearerToken(token);
    //        }
    //        return httpClient;
    //    }

    //    private void AddHeaderParameter(HttpClient httpClient, string parameter)
    //    {
    //        if (httpClient == null)
    //            return;

    //        if (string.IsNullOrEmpty(parameter))
    //            return;

    //        httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
    //    }

    //    private void AddBasicAuthenticationHeader(HttpClient httpClient, string clientId, string clientSecret)
    //    {
    //        if (httpClient == null)
    //            return;

    //        if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
    //            return;


    //        httpClient.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(clientId, clientSecret);
    //    }

    //    private async Task HandleResponse(HttpResponseMessage response)
    //    {
    //        if (!response.IsSuccessStatusCode)
    //        {
    //            var content = await response.Content.ReadAsStringAsync();

    //            if (response.StatusCode == HttpStatusCode.Forbidden ||
    //                response.StatusCode == HttpStatusCode.Unauthorized)
    //            {
    //                throw new ServiceAuthenticationException(content);
    //            }

    //            throw new HttpRequestExceptionEx(response.StatusCode, content);
    //        }
    //    }



    //}
}
