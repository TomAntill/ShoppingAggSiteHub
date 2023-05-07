using ShoppingAggSite.DAL;
using ShoppingAggSite.DataModels;
using ShoppingAggSiteHub.DTO.Store;
using ShoppingAggSiteHub.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingAggSiteUnitTests
{
    public class StoreAddTests
    {

        private UnitTestHelper _unitTestHelper = null;

        public StoreAddTests()
        {
            _unitTestHelper = new UnitTestHelper();
        }

        [Fact]
        public async void Store_Add_Success_HappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Store_Add_Success_HappyPath");
            StoreDAL storeDAL = new StoreDAL(contextForTest);

            var storeAddVm = new StoreAddDTO
            {
                BrandId = 10,
                LocationId = 1,
                StoreImageUrl = "http://www.test.co.uk/picture",
                StoreName = "Test Name"
            };

            // act
            var act = await storeDAL.Add(storeAddVm);

            // assert
            Assert.Equal(1, act);
        }
        [Fact]
        public async void Store_Add_Failure_Url_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Store_Add_Failure_Url_UnhappyPath");
            StoreDAL storeDAL = new StoreDAL(contextForTest);

            var storeAddVm = new StoreAddDTO
            {
                BrandId = 10,
                LocationId = 1,
                StoreImageUrl = "NotValidUrl",
                StoreName = "Test Name"
            };

            // act
            Func<Task<int>> act = async () => await storeDAL.Add(storeAddVm);

            // assert
            await Assert.ThrowsAsync<UrlNotValidException>(act);
        }
    }
}
