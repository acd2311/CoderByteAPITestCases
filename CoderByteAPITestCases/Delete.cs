using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
namespace CoderByteAPITestCases
{
    public class Delete
    {
        [Test]
        public void VerifyDeleteSKU_ValidID()
        {
            string result = APIMethods.ListSKU();

            //Get the existing sku record
            List<SKU> skus = JsonConvert.DeserializeObject<List<SKU>>(result);
            string inputSku = skus[0].sku;

            //Delete sku record
            bool isDeleted = APIMethods.DeleteSKU(inputSku);

            if (isDeleted)
            {
                //Verify deleted record
                result = APIMethods.GetSKU(inputSku);
                var strSkuDeleted = JObject.Parse(result)["Item"];
                Assert.IsNull(strSkuDeleted, "API returned valid record for Deleted sku id. Record returned: " + strSkuDeleted);
            }
            else
                Assert.Fail("Record is not deleted successfully for sku " + inputSku);
        }

        [Test]
        public void VerifyDeleteSKU_InvalidIDWithoutSpecialChars()
        {

            string invalidSkuID = "InvalidSku";
            // Delete sku record
            bool isDeleted = APIMethods.DeleteSKU(invalidSkuID);

            // Got response from Team as this is expected behaviour:
            // "When DELETE is called with non existing sku, API response returns OK. Is this as expected? Or should it be Forbidden?"

            if (isDeleted)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void VerifyDeleteSKU_InvalidIDWithSpecialChars()
        {

            string invalidSkuID = "InvalidSku-:!@";
            // Delete sku record
            bool isDeleted = APIMethods.DeleteSKU(invalidSkuID);

            if (isDeleted)
                Assert.Pass();
            else
                Assert.Fail("Expected API response is OK. Instead, received Forbidden for invalid sku ID " + invalidSkuID);

        }

        [Test]
        public void VerifyDeleteSKU_WithNullValue()
        {

            string invalidSkuID = null;
            // Delete sku record
            bool isDeleted = APIMethods.DeleteSKU(invalidSkuID);

            // Got response from Team as this is expected behaviour:
            // "When I do not provide SKU ID for DELETE, I get Forbidden status with message ‘Missing Authentication Token’
            // rather than missing sku or similar message. Is this as expected?"

            if (isDeleted)
                Assert.Fail("Record is deleted for invalid sku ID " + invalidSkuID);
            else
                Assert.Pass();
        }
    }
}
