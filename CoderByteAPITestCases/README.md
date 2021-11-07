# CoderByteAPITestCase
This project includes API test cases to verify CRUD operations on SKU API enpoint
It verifies follow scenarios:

## Read
1. VerifyGetAllRecords - It verifies that GET method returns more than 0 records
2. VerifyGetRecordByExistingFirstID_Valid - It gets list of all SKU records and verifies that GET method returns SKU records based on last sku record
3. VerifyGetRecordByExistingLastID_Valid - It gets list of all SKU records and verifies that GET method returns SKU records based on last sku record
4. VerifyGetRecord_InValidID - It verifies that API sends empty record for InvalidID

## Execute
To execute the test suite, navigate to project folder and execute `dotnet test CoderByteAPITestCases`.