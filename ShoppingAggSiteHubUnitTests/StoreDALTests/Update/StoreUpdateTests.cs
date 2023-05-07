using ShoppingAggSite.DAL;
using ShoppingAggSite.DataModels;
using ShoppingAggSiteHub.DTO.Store;
using ShoppingAggSiteHub.Exceptions;
using ShoppingAggSiteUnitTests;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingAggSiteHubUnitTests.StoreDALTests.Update
{
    public class StoreUpdateTests
    {
        private UnitTestHelper _unitTestHelper = null;

        public StoreUpdateTests()
        {
            _unitTestHelper = new UnitTestHelper();
        }
        [Fact]
        public async void Store_Update_Success_HappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Store_Update_Success_HappyPath");
            StoreDAL storeDAL = new StoreDAL(contextForTest);
            await _unitTestHelper.AddStoreToContext(contextForTest);
            Store store = contextForTest.Store.First();

            StoreUpdateDTO storeUpdateVm = new StoreUpdateDTO
            {
                StoreId = store.Id,
                BrandId = store.BrandId,
                LocationId = store.LocationId,
                StoreImageUrl = "https://www.updatedtest.co.uk/pic",
                StoreName = "Test Updated Name"
            };

            // act
            bool updated = await storeDAL.Update(storeUpdateVm);

            // assert
            Assert.True(updated);
            Assert.Equal("https://www.updatedtest.co.uk/pic", store.StoreImageUrl);
            Assert.Equal("Test Updated Name", store.StoreName);
        }
        [Fact]
        public async void Store_Update_Failure_UrlNotValid_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Store_Update_Failure_UrlNotValid_UnhappyPath");
            StoreDAL storeDAL = new StoreDAL(contextForTest);
            await _unitTestHelper.AddStoreToContext(contextForTest);
            Store store = contextForTest.Store.First();
            
            StoreUpdateDTO storeUpdateVm = new StoreUpdateDTO
            {
                StoreId = store.Id,
                BrandId = store.BrandId,
                LocationId = store.LocationId,
                StoreImageUrl = "UrlNotValid",
                StoreName = store.StoreName
            };

            // act
            Func<Task<bool>> act = async () => await storeDAL.Update(storeUpdateVm);

            // assert
            await Assert.ThrowsAsync<UrlNotValidException>(act);
        }
        [Fact]
        public async void Store_Update_Failure_EntityNotFound_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Store_Update_Failure_EntityNotFound_UnhappyPath");
            StoreDAL storeDAL = new StoreDAL(contextForTest);
            await _unitTestHelper.AddStoreToContext(contextForTest);
            Store store = contextForTest.Store.First();
            
            StoreUpdateDTO storeUpdateVm = new StoreUpdateDTO
            {
                StoreId = 999,
                BrandId = store.BrandId,
                LocationId = store.LocationId,
                StoreImageUrl = "https://www.updatedtest.co.uk/pic",
                StoreName = "Test Updated Name"
            };

            // act
            Func<Task<bool>> act = async () => await storeDAL.Update(storeUpdateVm);

            // assert
            await Assert.ThrowsAsync<EntityNotFoundException>(act);
        }
    }
}
