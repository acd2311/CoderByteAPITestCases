using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace CoderByteAPITestCases
{
     
    public class APIMethods
    {
        
        private const string url = "https://1ryu4whyek.execute-api.us-west-2.amazonaws.com/dev/skus";

        /// <summary>This method gets the string of SKU objects from the API Response.
        /// It throws exception if response is not 'OK'</summary>
        /// <returns>A string representing an API response.</returns>
        public static string ListSKU()
        {
            string result;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();

            if ((int)response.StatusCode == 200) //OK
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

        /// <summary>This method gets the string of SKU object from the API Response for 
        ///    (<paramref name="skuID"/>).</summary>
        /// <param name="skuID">SKU ID</param>
        /// <returns>A string representing an API response for the given (<paramref name="skuID"/></returns>
        public static string GetSKU(string skuID)
        {
            string result;
            string urlWithSKUID = url + "/" + skuID;
            var request = (HttpWebRequest)WebRequest.Create(urlWithSKUID);
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            if ((int)response.StatusCode == 200) //OK
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

        /// <summary>This method performs POST operation. It upserts (inserts or updates) the string of SKU object from the API Response for 
        ///    (<paramref name="skuObj"/>). </summary>
        /// <param name="skuObj">SKU object with the parameter values</param>
        /// <returns>A string representing an API response for the newly created or updated (<paramref name="skuObj"/>)</returns>
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

        /// <summary>This method performs DELETE operation on the 
        ///    (<paramref name="skuID"/>,<paramref name="yor"/>).
        /// It throws exception if response is not 'Forbidden' for invalid SKU ID. </summary>
        /// <param name="skuID">SKU ID</param>
        /// <returns>A 'true' value if DELETE operation is successful. A 'false' value in case it is unsuccessful.</returns>
        public static bool DeleteSKU(string skuID)
        {
            string urlWithSKUID = url + "/" + skuID;
            var request = (HttpWebRequest)WebRequest.Create(urlWithSKUID);
            request.Method = "DELETE";
            request.ContentType = "application/json";
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                if ((int)response.StatusCode == 200) //OK
                    return true;
                else
                    return false;
                
            }
            catch (WebException exception)
            {
                var response = (HttpWebResponse)exception.Response;
                if ((int)response.StatusCode == 403) //"Forbidden
                    return false;
                else
                    throw new Exception("Response code is not Forbidden for invalid sku ID '" + skuID + "'. Received response code = " + response.StatusCode.ToString());

            }
        }
    }
}
