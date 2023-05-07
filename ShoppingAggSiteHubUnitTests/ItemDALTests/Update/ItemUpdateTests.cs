using Microsoft.EntityFrameworkCore;
using ShoppingAggSite.DAL;
using ShoppingAggSite.DataModels;
using ShoppingAggSiteHub.DTO.Item;
using ShoppingAggSiteHub.Exceptions;
using ShoppingAggSiteUnitTests;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingAggSiteHubUnitTests.Update
{
    public class ItemUpdateTests
    {
        public readonly ShoppingContext ShoppingContext;

        private readonly UnitTestHelper _unitTestHelper = null;

        public ItemUpdateTests()
        {
            _unitTestHelper = new UnitTestHelper();
        }

        [Fact]
        public async void Item_Update_Success_HappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Update_Success_HappyPath");
            await _unitTestHelper.AddItemToContext(contextForTest);
            ItemDAL itemDAL = new ItemDAL(contextForTest);
            Item item = await contextForTest.Item.FirstAsync();
            ItemUpdateDTO itemUpdateVm = new ItemUpdateDTO
            {
                ItemId = item.Id,
                StoreId = item.StoreId,
                QualityRatingId = item.QualityRatingId,
                ItemName = item.ItemName,
                ItemImageUrl = "https://www.updatedtest.co.uk/pic",
                CurrentPrice = 45.99m,
                Weight = item.Weight
            };
            
            // act
            bool updated = await itemDAL.Update(itemUpdateVm);

            // assert
            Assert.True(updated);
            Assert.Equal("https://www.updatedtest.co.uk/pic", item.ItemImageUrl);
            Assert.Equal(45.99m, item.CurrentPrice);
        }

        [Fact]
        public async void Item_Update_Failure_UrlNotValid_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Update_Failure_UrlNotValid_UnhappyPath");
            await _unitTestHelper.AddItemToContext(contextForTest);
            ItemDAL itemDAL = new ItemDAL(contextForTest);
            Item item = await contextForTest.Item.FirstAsync();            
            ItemUpdateDTO itemUpdateVm = new ItemUpdateDTO
            {
                ItemId = item.Id,
                StoreId = item.StoreId,
                QualityRatingId = item.QualityRatingId,
                ItemName = item.ItemName,
                ItemImageUrl = "NotUrl",
                CurrentPrice = item.CurrentPrice,
                Weight = item.Weight
            };

            // act
            Func<Task<bool>> act = async () => await itemDAL.Update(itemUpdateVm);

            // assert
            await Assert.ThrowsAsync<UrlNotValidException>(act);
        }
        [Fact]
        public async void Item_Update_Failure_NegativeWeight_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Update_Failure_NegativeWeight_UnhappyPath");
            await _unitTestHelper.AddItemToContext(contextForTest);
            ItemDAL itemDAL = new ItemDAL(contextForTest);
            Item item = await contextForTest.Item.FirstAsync();
            ItemUpdateDTO itemUpdateVm = new ItemUpdateDTO
            {
                ItemId = item.Id,
                StoreId = item.StoreId,
                QualityRatingId = item.QualityRatingId,
                ItemName = item.ItemName,
                ItemImageUrl = item.ItemImageUrl,
                CurrentPrice = item.CurrentPrice,
                Weight = -10.00m
            };

            // act
            Func<Task<bool>> act = async () => await itemDAL.Update(itemUpdateVm);

            // assert
            await Assert.ThrowsAsync<PositiveValueException>(act);
        }
        [Fact]
        public async void Item_Update_Failure_NegativePrice_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Update_Failure_NegativePrice_UnhappyPath");
            await _unitTestHelper.AddItemToContext(contextForTest);
            ItemDAL itemDAL = new ItemDAL(contextForTest);
            Item item = await contextForTest.Item.FirstAsync();
            ItemUpdateDTO itemUpdateVm = new ItemUpdateDTO
            {
                ItemId = item.Id,
                StoreId = item.StoreId,
                QualityRatingId = item.QualityRatingId,
                ItemName = item.ItemName,
                ItemImageUrl = item.ItemImageUrl,
                CurrentPrice = -9.99m,
                Weight = item.Weight
            };

            // act
            Func<Task<bool>> act = async () => await itemDAL.Update(itemUpdateVm);

            // assert
            await Assert.ThrowsAsync<PositiveValueException>(act);
        }
        [Fact]
        public async void Item_Update_Failure_PriceFormat_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Update_Failure_PriceFormat_UnhappyPath");
            await _unitTestHelper.AddItemToContext(contextForTest);
            ItemDAL itemDAL = new ItemDAL(contextForTest);
            Item item = await contextForTest.Item.FirstAsync();
            ItemUpdateDTO itemUpdateVm = new ItemUpdateDTO
            {
                ItemId = item.Id,
                StoreId = item.StoreId,
                QualityRatingId = item.QualityRatingId,
                ItemName = item.ItemName,
                ItemImageUrl = item.ItemImageUrl,
                CurrentPrice = 9.993m,
                Weight = item.Weight
            };

            // act
            Func<Task<bool>> act = async () => await itemDAL.Update(itemUpdateVm);

            // assert
            await Assert.ThrowsAsync<PriceFormatException>(act);
        }
        [Fact]
        public async void Item_Update_Failure_EntityNotFound_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Update_Failure_EntityNotFound_UnhappyPath");
            await _unitTestHelper.AddItemToContext(contextForTest);
            ItemDAL itemDAL = new ItemDAL(contextForTest);
            Item item = await contextForTest.Item.FirstAsync();
            ItemUpdateDTO itemUpdateVm = new ItemUpdateDTO
            {
                ItemId = item.Id,
                StoreId = item.StoreId,
                QualityRatingId = item.QualityRatingId,
                ItemName = item.ItemName,
                ItemImageUrl = item.ItemImageUrl,
                CurrentPrice = 9.99m,
                Weight = item.Weight
            };

            await itemDAL.Delete(itemUpdateVm.ItemId);

            // act
            Func<Task<bool>> act = async () => await itemDAL.Update(itemUpdateVm);

            // assert
            await Assert.ThrowsAsync<EntityNotFoundException>(act);
        }
    }
}
