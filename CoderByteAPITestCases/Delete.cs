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
        public void VerifyDeleteSKU_InvalidID()
        {

            string invalidSkuID = "InvalidSku";
            // TODO: This test will fail currently. Handle success/failure based on response from team about API design.

            // Delete sku record
            bool isDeleted = APIMethods.DeleteSKU(invalidSkuID);

            if (isDeleted)
                Assert.Fail("Record is deleted for invalid sku ID " + invalidSkuID);
            else
                Assert.Pass();
        }
    }
}
