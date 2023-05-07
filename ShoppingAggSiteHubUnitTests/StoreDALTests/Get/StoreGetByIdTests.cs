using ShoppingAggSite.DAL;
using ShoppingAggSite.DataModels;
using ShoppingAggSiteHub.Exceptions;
using ShoppingAggSiteUnitTests;
using System;
using ShoppingAggSiteHub.DTO.Store;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingAggSiteHubUnitTests.StoreDALTests.GetById
{
    public class StoreGetByIdTests
    {
        private UnitTestHelper _unitTestHelper = null;

        public StoreGetByIdTests()
        {
            _unitTestHelper = new UnitTestHelper();
        }
        [Fact]
        public async void Store_GetById_Success_HappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Store_GetById_Success_HappyPath");
            StoreDAL storeDAL = new StoreDAL(contextForTest);
            await _unitTestHelper.AddStoreToContext(contextForTest);

            // act
            var act = await storeDAL.GetById(1);

            // assert
            Assert.Equal(1, act.Id);
        }
        [Fact]
        public async void Store_Update_Failure_EntityNotFound_UnhappyPath()
        {
            //arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Store_Update_Failure_EntityNotFound_UnhappyPath");
            StoreDAL storeDAL = new StoreDAL(contextForTest);
            await _unitTestHelper.AddStoreToContext(contextForTest);

            //act
            Func<Task<StoreDTO>> act = async () => await storeDAL.GetById(99);

            //assert
            await Assert.ThrowsAsync<EntityNotFoundException>(act);
        }
    }
}
