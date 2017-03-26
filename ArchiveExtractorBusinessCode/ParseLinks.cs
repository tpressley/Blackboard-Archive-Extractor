using System;
using System.Net;

namespace ArchiveExtractorBusinessCode
{
    public class ParseLinks
    {
        public string Uri { get; set; }                 // initial URI passed to this class to start request
        public string FinalUri { get; private set; }    // final URI that the response returns

        public ParseLinks(string uri)
        {
            Uri = uri;
            FinalUri = "";
        }

        /// <summary>
        /// HTTP GET request function for URI. 
        /// Returns true if resp code is 200 otherwise considered dead.
        /// Some things it should be equipped to handle is Monitor Connectivity.
        /// </summary>
        /// <returns>Boolean of request if alive or not</returns>
        public bool RequestUrl()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(Uri);
                request.AllowAutoRedirect = true;
                request.UserAgent = "Mozila/5.0";
                request.Timeout = 10000;    //10s

                var resp = (HttpWebResponse)request.GetResponse();
                FinalUri = resp.ResponseUri.ToString();
                Console.WriteLine((int)resp.StatusCode);

                if (IsAlive((int)resp.StatusCode))
                {
                    Console.WriteLine("ENDED WITH 200");
                    return true;
                }
                return false;
            }
            catch (WebException e)
            {
                Console.WriteLine("ERR: " + e);
                if (e.Status == WebExceptionStatus.ProtocolError)
                {   

                    var response = e.Response as HttpWebResponse;
                    if (response != null)
                    {
                        var statusCode = (int)response.StatusCode;
                        Console.WriteLine("HTTP Status Code: " + statusCode);
                        return IsAlive(statusCode);
                    }
                    return false;
                    // no http status code available
                }
                return false;
                // no http status code available
            }
        }

        /// <summary>
        /// Helper to check if Uri is an absolute path
        /// </summary>
        /// <param name="uri">URI to be checked if it is an absolute URI</param>
        /// <returns>Boolean of whether it is a an absolute URI or not</returns>
        public bool IsAbsoluteUri(string uri)
        {
            Uri result;
            return System.Uri.TryCreate(uri, UriKind.Absolute, out result);
        }

        private bool IsAlive(int code)
        {
            if(code == 200)
            {
                return true;
            }
            return false;
        }

    }

}
