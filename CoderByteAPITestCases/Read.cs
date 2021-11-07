using System.IO;
using System.Net;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CoderByteAPITestCases
{
    public class Read
    {
        
        [Test]
        public void VerifyGetAllRecords()
        {
            string result = APIMethods.GetAPIResponse();
            List<SKU> skus = JsonConvert.DeserializeObject<List<SKU>>(result);
            Assert.IsNotEmpty(skus, "Received zero records when trying to list records");
         }

        [Test]
        public void VerifyGetRecordByExistingFirstID_Valid()
        {
            string result = APIMethods.GetAPIResponse();
        
            List<SKU> skus = JsonConvert.DeserializeObject<List<SKU>>(result);
            string inputSku = skus[0].sku;

            result = APIMethods.GetAPIResponse(inputSku);
            var strSku = JObject.Parse(result)["Item"].ToString();

            SKU skuResponseDetails = JsonConvert.DeserializeObject<SKU>(strSku);

            Assert.AreEqual(inputSku, skuResponseDetails.sku, "API returned incorrect record for " + inputSku + "'. " +
                "Actual returned record sku ID = '" + skuResponseDetails.sku + "'");

        }

        [Test]
        public void VerifyGetRecordByExistingLastID_Valid()
        {
            string result = APIMethods.GetAPIResponse();

            List<SKU> skus = JsonConvert.DeserializeObject<List<SKU>>(result);
            string inputSku = skus[skus.Count-1].sku;

            result = APIMethods.GetAPIResponse(inputSku);
            var strSku = JObject.Parse(result)["Item"].ToString();

            SKU skuResponseDetails = JsonConvert.DeserializeObject<SKU>(strSku);

            Assert.AreEqual(inputSku, skuResponseDetails.sku, "API returned incorrect record for " + inputSku + "'. " +
                "Actual returned record sku ID = '" + skuResponseDetails.sku + "'");

        }

        [Test]
        public void VerifyGetRecord_InValidID()
        {
            string result = APIMethods.GetAPIResponse("InvalidSKUID");
            var strSku = JObject.Parse(result)["Item"];
            Assert.IsNull(strSku,"API returned valid record for Invalid sku id. Record returned: " + strSku);
        }


    }
}
