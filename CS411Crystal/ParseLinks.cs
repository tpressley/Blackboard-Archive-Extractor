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
        public string uri { get; set; }                 // initial URI passed to this class to start request
        public string finalUri { get; private set; }    // final URI that the response returns

        public ParseLinks(string uri)
        {
            this.uri = uri;
            this.finalUri = "";
        }

        /// <summary>
        /// HTTP GET request function for URI. 
        /// Returns true if resp code is 200 otherwise considered dead.
        /// Some things it should be equipped to handle is Monitor Connectivity.
        /// </summary>
        /// <returns>Boolean of request if alive or not</returns>
        public bool requestUrl()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.uri);
                request.AllowAutoRedirect = true;
                request.UserAgent = "Mozila/5.0";
                request.Timeout = 10000;    //10s

                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                finalUri = resp.ResponseUri.ToString();
                Console.WriteLine((int)resp.StatusCode);

                if (isAlive((int)resp.StatusCode))
                {
                    Console.WriteLine("ENDED WITH 200");
                    return true;
                }
                return false;
            }
            catch (WebException e)
            {
                Console.WriteLine("ERR: " + e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {   

                    var response = e.Response as HttpWebResponse;
                    if (response != null)
                    {
                        var statusCode = (int)response.StatusCode;
                        Console.WriteLine("HTTP Status Code: " + statusCode);
                        return isAlive(statusCode);
                    }
                    else
                    {
                        return false;
                        // no http status code available
                    }
                }
                else
                {
                    return false;
                    // no http status code available
                }

            }
        }

        /// <summary>
        /// Helper to check if Uri is an absolute path
        /// </summary>
        /// <param name="uri">URI to be checked if it is an absolute URI</param>
        /// <returns>Boolean of whether it is a an absolute URI or not</returns>
        public bool isAbsoluteUri(string uri)
        {
            Uri result;
            return Uri.TryCreate(uri, UriKind.Absolute, out result);
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
