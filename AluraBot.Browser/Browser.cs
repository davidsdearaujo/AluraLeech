using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AluraBot.Browser
{
    internal class Browser
    {
        CookieContainer cookies = new CookieContainer();

        public CookieContainer GetCookie()
        {
            return cookies;
        }

        public string Navigate(string url, Dictionary<string, string> postData = null)
        {
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);

            getRequest.CookieContainer = cookies;
            getRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            getRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            getRequest.AllowWriteStreamBuffering = true;
            getRequest.ProtocolVersion = HttpVersion.Version11;
            getRequest.AllowAutoRedirect = true;
            getRequest.ContentType = "application/x-www-form-urlencoded";

            if (postData != null)
            {
                var strPostData = string.Join("&", postData.Select(a => $"{a.Key}={System.Net.WebUtility.UrlEncode(a.Value)}"));
                getRequest.Method = WebRequestMethods.Http.Post;
                byte[] byteArray = Encoding.ASCII.GetBytes(strPostData);
                getRequest.ContentLength = byteArray.Length;
                Stream newStream = getRequest.GetRequestStream(); //open connection
                newStream.Write(byteArray, 0, byteArray.Length); // Send the data.
                newStream.Close();
            }
            else
            {
                getRequest.Method = WebRequestMethods.Http.Get;
            }

            System.Net.ServicePointManager.Expect100Continue = false;
            HttpWebResponse getResponse = (HttpWebResponse)getRequest.GetResponse();
            var retorno = new StringBuilder();
            using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }


        public string RequisicaoVideo(string urlSolicitacaoVIdeo)
        {
            string linkVideo;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlSolicitacaoVIdeo);
            request.CookieContainer = cookies;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            request.AllowWriteStreamBuffering = true;
            request.ProtocolVersion = HttpVersion.Version11;
            request.AllowAutoRedirect = true;
            request.ContentType = "application/x-www-form-urlencoded";
            System.Net.ServicePointManager.Expect100Continue = false;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                linkVideo = reader.ReadToEnd();
            }

            return linkVideo;
        }
    }
}
