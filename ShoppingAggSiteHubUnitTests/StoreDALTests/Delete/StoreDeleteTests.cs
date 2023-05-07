using ShoppingAggSite.DAL;
using ShoppingAggSite.DataModels;
using ShoppingAggSiteHub.Exceptions;
using ShoppingAggSiteUnitTests;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingAggSiteHubUnitTests.StoreDALTests.Delete
{
    public class StoreDeleteTests
    {
        private UnitTestHelper _unitTestHelper = null;

        public StoreDeleteTests()
        {
            _unitTestHelper = new UnitTestHelper();
        }
        [Fact]
        public async void Store_Delete_Success_HappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Store_Delete_Success_HappyPath");
            StoreDAL storeDAL = new StoreDAL(contextForTest);
            await _unitTestHelper.AddStoreToContext(contextForTest);
            Store store = contextForTest.Store.First();

            // act
            bool success = await storeDAL.Delete(store.Id);

            // assert
            Assert.True(success);
        }
        [Fact]
        public async void Store_Delete_Failure_NoEntityFound_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Store_Delete_Failure_NoEntityFound_UnhappyPath");
            StoreDAL storeDAL = new StoreDAL(contextForTest);
            await _unitTestHelper.AddStoreToContext(contextForTest);
            Store store = contextForTest.Store.First();

            // act
            Func<Task<bool>> act = async () => await storeDAL.Delete(99);

            // assert
            await Assert.ThrowsAsync<EntityNotFoundException>(act);
        }
    }
}
