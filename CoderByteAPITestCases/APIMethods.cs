using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace CoderByteAPITestCases
{
    public class APIMethods
    {
        private const string url = "https://1ryu4whyek.execute-api.us-west-2.amazonaws.com/dev/skus";

        public static string ListSKU()
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

        public static string GetSKU(string skuID)
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

        public static string UpsertSKU(SKU newSku)
        {
            string strVerify;
            string insert = JsonConvert.SerializeObject(newSku);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(insert);
            }
            var response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream))
            {
                strVerify = reader.ReadToEnd();
            }

            return strVerify;
        }

        public static bool DeleteSKU(string skuID)
        {
            string urlWithSKUID = url + "/" + skuID;
            var request = (HttpWebRequest)WebRequest.Create(urlWithSKUID);
            request.Method = "DELETE";
            request.ContentType = "application/json";
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode.ToString().Equals("OK"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (WebException exception)
            {
                var response = (HttpWebResponse)exception.Response;
                if (response.StatusCode.ToString().Equals("Forbidden"))
                    return false;
                else
                    throw new Exception("Response code is not Forbidden for invalid sku ID '" + skuID + "'. Received response code = " + response.StatusCode.ToString());

            }
        }
    }
}
