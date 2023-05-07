using ShoppingAggSite.DAL;
using ShoppingAggSite.DataModels;
using ShoppingAggSiteHub.DTO.Item;
using ShoppingAggSiteHub.Exceptions;
using ShoppingAggSiteUnitTests;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingAggSiteHubUnitTests.ItemDALTests.Add
{
    public class ItemAddTests
    {
        private readonly UnitTestHelper _unitTestHelper = null;

        public ItemAddTests()
        {
            _unitTestHelper = new UnitTestHelper();
        }
        [Fact]
        public async void Item_Add_Success_HappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Add_Success_HappyPath");
            await _unitTestHelper.AddItemToContext(contextForTest);
            ItemDAL itemDAL = new ItemDAL(contextForTest);

            var itemAddVm = new ItemAddDTO
            {
                StoreId = 1,
                QualityRatingId = 1,
                ItemImageUrl = "https://www.test.co.uk/picture",
                ItemName = "Test Name",
                CurrentPrice = 3,
                Weight = 5
            };

            // act
            var act = await itemDAL.Add(itemAddVm);

            // assert
            Assert.Equal(contextForTest.Item.Max(x => x.Id), act);
        }
        [Fact]
        public async void Item_Add_Failure_Price_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Add_Failure_Price_UnhappyPath");
            await _unitTestHelper.AddItemToContext(contextForTest);
            ItemDAL itemDAL = new ItemDAL(contextForTest);

            var itemAddVm = new ItemAddDTO
            {
                StoreId = 1,
                QualityRatingId = 1,
                ItemImageUrl = "https://www.test.co.uk/picture",
                ItemName = "Test Name",
                CurrentPrice = 3.287m,
                Weight = 5
            };

            // act
            Func<Task<int>> act = async () => await itemDAL.Add(itemAddVm);

            // assert
            await Assert.ThrowsAsync<PriceFormatException>(act);
        }
        [Fact]
        public async void Item_Add_Failure_Url_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Add_Failure_Url_UnhappyPath");
            await _unitTestHelper.AddItemToContext(contextForTest);
            ItemDAL itemDAL = new ItemDAL(contextForTest);

            var itemAddVm = new ItemAddDTO
            {
                StoreId = 1,
                QualityRatingId = 1,
                ItemImageUrl = "UrlNotValid",
                ItemName = "Test Name",
                CurrentPrice = 3.28m,
                Weight = 5
            };

            // act
            Func<Task<int>> act = async () => await itemDAL.Add(itemAddVm);

            // assert
            await Assert.ThrowsAsync<UrlNotValidException>(act);
        }
        [Fact]
        public async void Item_Add_Failure_NegativePrice_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Add_Failure_NegativePrice_UnhappyPath");
            ItemDAL itemDAL = new ItemDAL(contextForTest);

            var itemAddVm = new ItemAddDTO
            {
                StoreId = 1,
                QualityRatingId = 1,
                ItemImageUrl = "https://www.test.co.uk/picture",
                ItemName = "Test Name",
                CurrentPrice = -3.28m,
                Weight = 5
            };

            // act
            Func<Task<int>> act = async () => await itemDAL.Add(itemAddVm);

            // assert
            await Assert.ThrowsAsync<PositiveValueException>(act);
        }
        [Fact]
        public async void Item_Add_Failure_NegativeWeight_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Add_Failure_NegativeWeight_UnhappyPath");
            ItemDAL itemDAL = new ItemDAL(contextForTest);

            var itemAddVm = new ItemAddDTO
            {
                StoreId = 1,
                QualityRatingId = 1,
                ItemImageUrl = "https://www.test.co.uk/picture",
                ItemName = "Test Name",
                CurrentPrice = 3.28m,
                Weight = -5
            };

            // act
            Func<Task<int>> act = async () => await itemDAL.Add(itemAddVm);

            // assert
            await Assert.ThrowsAsync<PositiveValueException>(act);
        }
    }
}
