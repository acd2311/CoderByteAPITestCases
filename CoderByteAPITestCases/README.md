# CoderByteAPITestCase
This project includes API test cases to verify CRUD operations on SKU API enpoint
It verifies follow scenarios:

## Create
1. VerifyInsertNewSKU_ValidData - It verifies that new SKU record is inserted for all valid data
2. VerifyInsertNewSKU_NullData - It verifies that null SKU record is not getting inserted.

## Delete
1. VerifyDeleteSKU_ValidID - It verifies SKU record is deleted for exisitng valid sku ID
2. VerifyDeleteSKU_InvalidIDWithoutSpecialChars - It verifies DELETE operation returns 200 OK response for invalid SKU ID without special characters.
3. VerifyDeleteSKU_InvalidIDWithSpecialChars - It verifies DELETE operation returns 200 OK response for invalid SKU ID without special characters.
4. VerifyDeleteSKU_WithNullValue -  It verifies DELETE operation returns 403 Forbidden response for null SKU ID without special characters.

## Read
1. VerifyGetAllRecords - It verifies that GET method returns more than 0 records
2. VerifyGetRecordByExistingFirstID_Valid - It gets list of all SKU records and verifies that GET method returns SKU records based on last sku record
3. VerifyGetRecordByExistingLastID_Valid - It gets list of all SKU records and verifies that GET method returns SKU records based on last sku record
4. VerifyGetRecord_InValidID - It verifies that API sends empty record for InvalidID

## Update
1. VerifyUpdateSKU_DescriptionAndPrice - It verifies that existing SKU record is getting updated successfully when Description and Price are updated.
2. VerifyUpdateSKU_OnlyDescription - It verifies that existing SKU record is getting updated successfully when only Description is updated.
3. VerifyUpdateSKU_OnlyPrice - It verifies that existing SKU record is getting updated successfully when only price is updated.

## Execute
To execute the test suite, navigate to project folder and execute `dotnet test CoderByteAPITestCases`.