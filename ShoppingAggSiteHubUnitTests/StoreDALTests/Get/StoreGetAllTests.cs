using ShoppingAggSite.DAL;
using ShoppingAggSite.DataModels;
using ShoppingAggSiteUnitTests;
using System.Collections.Generic;
using ShoppingAggSiteHub.DTO.Store;
using Xunit;

namespace ShoppingAggSiteHubUnitTests.StoreDALTests.GetById
{
    public class StoreGetAllTests
    {
        private UnitTestHelper _unitTestHelper = null;

        public StoreGetAllTests()
        {
            _unitTestHelper = new UnitTestHelper();
        }
        [Fact]
        public async void Store_GetAll_Success_HappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Store_GetAll_Success_HappyPath");
            await _unitTestHelper.AddStoreToContext(contextForTest);
            await _unitTestHelper.AddStoreToContext(contextForTest);
            StoreDAL storeDAL = new StoreDAL(contextForTest);

            // act
            List<StoreDTO> act = await storeDAL.GetAll();

            // assert
            Assert.Collection(act,
            item =>
            {
                Assert.Equal(1, item.Id);
            },
            item =>
            {
                Assert.Equal(2, item.Id);
            });
        }        
    }
}
