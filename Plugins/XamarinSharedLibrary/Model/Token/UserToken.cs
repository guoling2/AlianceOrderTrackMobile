using System;
using Newtonsoft.Json;

namespace XamarinSharedLibrary.Model.Token
{
    public class UserToken
    {
        [JsonProperty("id_token")]
        public string IdToken { get; set; }

		[JsonProperty("access_token")]
        public string AccessToken { get; set; }

		[JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

		[JsonProperty("token_type")]
        public string TokenType { get; set; }

		[JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        public DateTime Authtime   { get; set; }

        public DateTime ExpTime { get; set; }
        public string SubId { get; set; }
        public bool IsExprie => ( ExpTime- DateTime.UtcNow ).Minutes <5 ? true : false;
    }
}
