using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
namespace CoderByteAPITestCases
{
    public class Update
    {
        [Test]
        public void VerifyUpdateSKU_DescriptionAndPrice()
        {
            string result = APIMethods.ListSKU();

            List<SKU> skus = JsonConvert.DeserializeObject<List<SKU>>(result);
            string inputSku = skus[0].sku; //Get the first sku record

            result = APIMethods.GetSKU(inputSku);
            var strSku = JObject.Parse(result)["Item"].ToString();

            //Update sku record with _Updated suffix
            SKU skuResponseDetails = JsonConvert.DeserializeObject<SKU>(strSku);
            
            skuResponseDetails.description += "_Updated";
            skuResponseDetails.price += "_Updated"; ;

            //Insert the updated sku record
            string insertedSku = APIMethods.UpsertSKU(skuResponseDetails);

            //Verify inserted sku record is as expected
            var apiResponse = JsonConvert.DeserializeObject<SKU>(insertedSku);
            Assert.IsTrue(apiResponse.description.Equals(skuResponseDetails.description) && apiResponse.price.Equals(skuResponseDetails.price),
                "Updated SKU object is not expected. " +
                "Expected description = {0}, Actual description = {1}. " +
                "Expected price = {2}, Actual price = {3}.", skuResponseDetails.description, apiResponse.description, skuResponseDetails.price, apiResponse.price);


        }

        [Test]
        public void VerifyUpdateSKU_OnlyDescription()
        {
            string result = APIMethods.ListSKU();

            List<SKU> skus = JsonConvert.DeserializeObject<List<SKU>>(result);
            string inputSku = skus[0].sku; //Get the first sku record

            result = APIMethods.GetSKU(inputSku);
            var strSku = JObject.Parse(result)["Item"].ToString();

            //Update sku record with _Updated suffix
            SKU skuResponseDetails = JsonConvert.DeserializeObject<SKU>(strSku);

            skuResponseDetails.description += "_Updated";
            

            //Insert the updated sku record
            string insertedSku = APIMethods.UpsertSKU(skuResponseDetails);

            //Verify inserted sku record is as expected
            var apiResponse = JsonConvert.DeserializeObject<SKU>(insertedSku);
            Assert.IsTrue(apiResponse.description.Equals(skuResponseDetails.description) ,
                "Updated SKU object is not expected. " +
                "Expected description = {0}, Actual description = {1}. "
                ,skuResponseDetails.description, apiResponse.description);


        }

        [Test]
        public void VerifyUpdateSKU_OnlyPrice()
        {
            string result = APIMethods.ListSKU();

            List<SKU> skus = JsonConvert.DeserializeObject<List<SKU>>(result);
            string inputSku = skus[0].sku; //Get the first sku record

            result = APIMethods.GetSKU(inputSku);
            var strSku = JObject.Parse(result)["Item"].ToString();

            //Update sku record with _Updated suffix
            SKU skuResponseDetails = JsonConvert.DeserializeObject<SKU>(strSku);

            skuResponseDetails.price += "-2.99";


            //Insert the updated sku record
            string insertedSku = APIMethods.UpsertSKU(skuResponseDetails);

            //Verify inserted sku record is as expected
            var apiResponse = JsonConvert.DeserializeObject<SKU>(insertedSku);
            Assert.IsTrue(apiResponse.price.Equals(skuResponseDetails.price),
                "Updated SKU object is not expected. " +
                "Expected price = {0}, Actual price = {1}. "
                , skuResponseDetails.price, apiResponse.price);


        }
    }
}
