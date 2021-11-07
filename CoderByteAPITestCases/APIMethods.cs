using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace CoderByteAPITestCases
{
    public class APIMethods
    {
        private const string url = "https://1ryu4whyek.execute-api.us-west-2.amazonaws.com/dev/skus";

        public static string GetAPIResponse()
        {
            string result;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode.ToString() == "OK")
            {
                Stream stream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            else
            {
                throw new Exception("Response code is not OK. Received response code = " + response.StatusCode.ToString());
            }
        }

        public static string GetAPIResponse(string skuID)
        {
            string result;
            string urlWithSKUID = url + "/" + skuID;
            var request = (HttpWebRequest)WebRequest.Create(urlWithSKUID);
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode.ToString() == "OK")
            {
                Stream stream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            else
            {
                throw new Exception("Response code is not OK for sku ID '" + skuID + "'. Received response code = " + response.StatusCode.ToString());
            }
        }
    }
}
