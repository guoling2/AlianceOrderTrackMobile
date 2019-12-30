using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinSharedLibrary.Model.Token
{
    public class UserRequest
    {


        public bool IsError { get; set; }


        public string ErrorMsg { get; set; }


        public UserToken UserToken { get; set; }
    }
}
