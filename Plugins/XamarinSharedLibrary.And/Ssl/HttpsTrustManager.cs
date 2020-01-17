using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Security;
using Java.Security.Cert;
using Javax.Net.Ssl;

namespace XamarinSharedLibrary.And.Ssl
{
    public class HttpsTrustManager : Java.Lang.Object, IX509TrustManager, IJavaObject, IDisposable
    {


        public void CheckClientTrusted(X509Certificate[] chain, string authType)
        {

        }

        public void CheckServerTrusted(X509Certificate[] chain, string authType)
        {

        }



        public X509Certificate[] GetAcceptedIssuers()
        {
            var xb = new X509Certificate[0];
            return xb;
        }

        public static SSLSocketFactory createSSLSocketFactory()
        {
            SSLSocketFactory sSLSocketFactory = null;
            try
            {
                SSLContext sc = SSLContext.GetInstance("TLS");
                sc.Init(null, new ITrustManager[] { new HttpsTrustManager() },
                    new SecureRandom());
                sSLSocketFactory = sc.SocketFactory;
            }
            catch (Exception e)
            {
            }

            return sSLSocketFactory;
        }


    }


    public class TrustAllHostnameVerifier : Java.Lang.Object, IHostnameVerifier, IJavaObject, IDisposable
    {





        public bool Verify(string hostname, ISSLSession session)
        {
            return true;
        }


    }
}