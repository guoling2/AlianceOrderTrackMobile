using System;
using System.Collections.Generic;
using System.Text;
using XamarinSharedLibrary.Help;
using XamarinSharedLibrary.Model.Token;

namespace XamarinSharedLibrary.IdentityModel
{
    public class GlobalSetting
    {
        public const string AzureTag = "Azure";
        public const string MockTag = "Mock";
        public const string DefaultEndpoint = "http://YOUR_IP_OR_DNS_NAME"; // i.e.: "http://YOUR_IP" or "http://YOUR_DNS_NAME"

        private string _baseIdentityEndpoint;
        //private string _baseGatewayShoppingEndpoint;
        //private string _baseGatewayMarketingEndpoint;
        private string _baseGatewayLogisticEndpoint;
        private  static GlobalSetting _instance =null;
        private GlobalSetting()
        {
            AuthToken = "INSERT AUTHENTICATION TOKEN";

            BaseIdentityEndpoint = DefaultEndpoint;
          //  GatewayLogisticEndpoint = DefaultEndpoint;
            //BaseGatewayShoppingEndpoint = DefaultEndpoint;
            //BaseGatewayMarketingEndpoint = DefaultEndpoint;
        }

        public static GlobalSetting Instance
        {
            get
            {

                if (_instance == null)
                {
                    _instance= new GlobalSetting();
                }
                return _instance;
            }
        }

        public string BaseIdentityEndpoint
        {
            get { return _baseIdentityEndpoint; }
            set
            {
                _baseIdentityEndpoint = value;
                UpdateEndpoint(_baseIdentityEndpoint);
            }
        }

        //public string BaseGatewayShoppingEndpoint
        //{
        //    get { return _baseGatewayShoppingEndpoint; }
        //    set
        //    {
        //        _baseGatewayShoppingEndpoint = value;
        //        UpdateGatewayShoppingEndpoint(_baseGatewayShoppingEndpoint);
        //    }
        //}

        //public string BaseGatewayMarketingEndpoint
        //{
        //    get { return _baseGatewayMarketingEndpoint; }
        //    set
        //    {
        //        _baseGatewayMarketingEndpoint = value;
        //        UpdateGatewayMarketingEndpoint(_baseGatewayMarketingEndpoint);
        //    }
        //}

        /// <summary>
        /// aliance节点
        /// </summary>
        public string GatewayLogisticEndpoint
        {

            get { return _baseGatewayLogisticEndpoint; }
            set
            {
                _baseGatewayLogisticEndpoint = value;
                UpdateGatewayLogisticEndpoint(_baseGatewayLogisticEndpoint);
            }

        }

        public  UserToken UserToken
        {
            get => PreferencesHelpers.Get(nameof(UserToken), default(UserToken));
            set => PreferencesHelpers.Set(nameof(UserToken), value);
        }

        public string ClientId { get { return "xamarin"; } }

        public string ClientSecret { get { return "secret"; } }

        public string AuthToken { get; set; }

        public string RegisterWebsite { get; set; }

        public string AuthorizeEndpoint { get; set; }

        public string UserInfoEndpoint { get; set; }

        public string TokenEndpoint { get; set; }

        public string LogoutEndpoint { get; set; }

        public string Callback { get; set; }

        public string LogoutCallback { get; set; }

        

      //  public string GatewayMarketingEndpoint { get; set; }

        private void UpdateEndpoint(string endpoint)
        {
            RegisterWebsite = $"{endpoint}/Account/Register";
            LogoutCallback = $"{endpoint}/Account/Redirecting";

            var connectBaseEndpoint = $"{endpoint}/connect";
            AuthorizeEndpoint = $"{connectBaseEndpoint}/authorize";
            UserInfoEndpoint = $"{connectBaseEndpoint}/userinfo";
            TokenEndpoint = $"{connectBaseEndpoint}/token";
            LogoutEndpoint = $"{connectBaseEndpoint}/endsession";

            var baseUri = ExtractBaseUri(endpoint);
            Callback = $"{baseUri}/xamarincallback";
        }

        //private void UpdateGatewayShoppingEndpoint(string endpoint)
        //{
        //    GatewayLogisticEndpoint = $"{endpoint}";
        //}

        //private void UpdateGatewayMarketingEndpoint(string endpoint)
        //{
        //    GatewayMarketingEndpoint = $"{endpoint}";
        //}
        private void UpdateGatewayLogisticEndpoint(string endpoint)
        {
            _baseGatewayLogisticEndpoint = $"{endpoint}";
        }

        private string ExtractBaseUri(string endpoint)
        {
            var uri = new Uri(endpoint);
            var baseUri = uri.GetLeftPart(System.UriPartial.Authority);

            return baseUri;
        }
    }
}
