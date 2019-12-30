using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using XamarinSharedLibrary.IdentityModel;

namespace XamarinSharedLibrary.Model.Token
{
   public class IdentityService
   {


       public static string UserIdCliaim = "sub";

       public static string UserAvatarUrlCliaim = "userheadimage";

       public static string UserRealNameCliaim = "realname";

        public IdentityService()
        {



         
        }

       
       public async Task<UserRequest> LoginAsync(string userId, string password)
       {
           var client = new HttpClient();


           var xxx = GlobalSetting.Instance;
           var a = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
           {
               UserName = userId,
               Password = password,
               Address = GlobalSetting.Instance.TokenEndpoint,
               ClientId = GlobalSetting.Instance.ClientId,
               ClientSecret = GlobalSetting.Instance.ClientSecret
           });




           if (a.IsError)
           {

               if (a.HttpStatusCode != System.Net.HttpStatusCode.OK)
               {
                   //网络错误
                   return new UserRequest()
                   {
                       IsError = false,
                       ErrorMsg = a.HttpErrorReason
                   };
               }
               else
               {
                   //认证方面的错误
                   if (a.Error == "invalid_grant")
                   {
                       return new UserRequest()
                       {
                           IsError = false,
                           ErrorMsg = "用户名或密码错误"
                       };

                   }
                   else
                   {
                       return new UserRequest()
                       {
                           IsError = false,
                           ErrorMsg = a.ErrorDescription
                       };
                   }
               }


           }
           else
           {
               var userRequest = new UserRequest
               {
                   IsError = false,
                   UserToken = a.CreateUserTokenInfo()
               };



               return userRequest;


           }


       }


   }


    public static class IdentityServicesExtension
    {


        public static UserToken CreateUserTokenInfo(this TokenResponse tokenResponse)
        {
            var tokenstring = tokenResponse.AccessToken;
            var re = tokenResponse.RefreshToken;


            var userToken = new UserToken();
            var handler = new JwtSecurityTokenHandler();
            var tokenresult = handler.ReadJwtToken(tokenstring);


            userToken.RefreshToken = re;
            userToken.Authtime = tokenresult.ValidFrom;
            userToken.ExpTime = tokenresult.ValidTo;
            userToken.AccessToken = tokenstring;
            userToken.ExpiresIn = tokenResponse.ExpiresIn;

            if (tokenresult.Payload.TryGetValue(IdentityService.UserIdCliaim.ToLower(), out var userIdCliaim))
            {
                userToken.SubId = userIdCliaim.ToString();
            }


            return userToken;
        }
        public static void Register(bool ismock=false)
        {

       
            Xamarin.Forms.DependencyService.Register<IdentityService>();
        }
    } 
}
