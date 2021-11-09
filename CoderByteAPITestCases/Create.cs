using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace CoderByteAPITestCases
{
    public class Create
    {
        [Test]
        public void VerifyInsertNewSKU_ValidData()
        {
            //Create new sku object
            var skuObject = new SKU();
            skuObject.sku = DateTime.Now.ToString(); //using current DateTime for unique value
            string description = "New SKU by test " + nameof(VerifyInsertNewSKU_ValidData);
            skuObject.description = description;
            string price = "12.12";
            skuObject.price = price;

            //Insert sku record
            string insertedSku = APIMethods.UpsertSKU(skuObject);

            //Verify inserted sku record
            var apiResponse = JsonConvert.DeserializeObject<SKU>(insertedSku);
            Assert.IsTrue(apiResponse.description.Equals(description) && apiResponse.price.Equals(price),
                "Inserted SKU object is not expected. " +
                "Expected description = {0}, Actual description = {1}. " +
                "Expected price = {2}, Actual price = {3}." ,description, apiResponse.description,price, apiResponse.price);
        }

        [Test]
        public void VerifyInsertNewSKU_NullData()
        {
            //Create new sku object
            var skuObject = new SKU();
            skuObject.sku = null;
            string description = null;
            skuObject.description = description;
            string price = null;
            skuObject.price = price;

            //Insert sku record
            try
            {
                string insertedSku = APIMethods.UpsertSKU(skuObject);

                //Verify inserted sku record
                var apiResponse = JsonConvert.DeserializeObject<SKU>(insertedSku);
                Assert.IsTrue(apiResponse.description.Equals(description) && apiResponse.price.Equals(price),
                    "Inserted SKU object is not expected. " +
                    "Expected description = {0}, Actual description = {1}. " +
                    "Expected price = {2}, Actual price = {3}.", description, apiResponse.description, price, apiResponse.price);

            }
            catch (WebException exception)
            {
                var response = (HttpWebResponse)exception.Response;
                if ((int)response.StatusCode == 502)    //Bad Gateway
                {
                    Stream stream = response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var result = reader.ReadToEnd();
                        var errorMessage = JObject.Parse(result)["message"].ToString();
                        Assert.IsTrue("Internal server error".Equals(errorMessage), "API returned expected 502 exception but unexpected error message. Actual error message was {0}", errorMessage);
                    }
                }
                else
                    Assert.Fail("Unexpected exception while inserting null sku record. Received response code = " + response.StatusCode.ToString());
            }
        }

        //TODO:more cases with invalid entries
        //> Haven't got answer for question regarding invalid entries (What are the invalid values for SKU ID, description and price. I want to verify POST for invalid data entry)


    }
}
