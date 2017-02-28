using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CS411Crystal
{
    public class ParseLinks
    {
        public string url { get; set; }

        public ParseLinks(string url)
        {
            this.url = url;
        }

        public void requestUrl(string url)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.url);
            request.AllowAutoRedirect = true;
            request.UserAgent = "Mozila/5.0";
            request.Timeout = 10000;    //10s

            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            if (isAlive((int)resp.StatusCode))
            {
                Console.WriteLine("AWESOME ITS ALIVE");
            }else
            {
                Console.WriteLine("NOT ALIVE");
            }
            // Display the status.  
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        }

        // Helpers
        private bool isAbsoluteUrl(string url)
        {
            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result);
        }

        private bool isAlive(int code)
        {
            if(code == 200)
            {
                return true;
            }
            return false;
        }

    }

}
