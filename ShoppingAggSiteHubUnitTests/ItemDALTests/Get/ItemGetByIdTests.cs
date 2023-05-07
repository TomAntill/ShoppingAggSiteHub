using ShoppingAggSite.DAL;
using ShoppingAggSite.DataModels;
using ShoppingAggSiteHub.DTO.Item;
using ShoppingAggSiteHub.Exceptions;
using ShoppingAggSiteUnitTests;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingAggSiteHubUnitTests.ItemDALTests.GetById
{
    public class ItemGetByIdTests
    {
        private readonly UnitTestHelper _unitTestHelper = null;

        public ItemGetByIdTests()
        {
            _unitTestHelper = new UnitTestHelper();
        }
        [Fact]
        public async void Item_GetById_Success_HappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_GetById_Success_HappyPath");
            await _unitTestHelper.AddItemToContext(contextForTest);
            Item item = contextForTest.Item.First();
            ItemDAL itemDAL = new ItemDAL(contextForTest);

            // act
            ItemDTO theItem = await itemDAL.GetById(item.Id);

            // assert
            Assert.Equal(item.StoreId, theItem.StoreId);
            Assert.Equal(item.ItemImageUrl, theItem.ItemImageUrl);
            Assert.Equal(item.QualityRatingId, theItem.QualityRatingId);
            Assert.Equal(item.ItemImageUrl, theItem.ItemImageUrl);
            Assert.Equal(item.Weight, theItem.Weight);
            Assert.Equal(item.CurrentPrice, theItem.CurrentPrice);
        }
        [Fact]
        public async void Item_Update_Failure_EntityNotFound_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Update_Failure_EntityNotFound_UnhappyPath");
            await _unitTestHelper.AddItemToContext(contextForTest);
            ItemDAL itemDAL = new ItemDAL(contextForTest);

            // act
            Func<Task<ItemDTO>> act = async () => await itemDAL.GetById((contextForTest.Item.Max(x => x.Id)+1));
            
            // assert
            await Assert.ThrowsAsync<EntityNotFoundException>(act);
        }        
    }
}
