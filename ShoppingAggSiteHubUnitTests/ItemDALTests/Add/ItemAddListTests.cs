using ShoppingAggSite.DAL;
using ShoppingAggSite.DataModels;
using ShoppingAggSiteHub.DTO.Item;
using ShoppingAggSiteUnitTests;
using System.Collections.Generic;
using Xunit;

namespace ShoppingAggSiteHubUnitTests.ItemDALTests.Add
{
    public class ItemAddListTests
    {
        private readonly UnitTestHelper _unitTestHelper = null;

        public ItemAddListTests()
        {
            _unitTestHelper = new UnitTestHelper();
        }
        [Fact]
        public async void Item_AddList_Success_HappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_AddList_Success_HappyPath");
            ItemDAL itemDAL = new ItemDAL(contextForTest);

            List<ItemAddDTO> itemAddVms = new List<ItemAddDTO>()
                {
                    new ItemAddDTO
                {
                    StoreId = 1,
                    QualityRatingId = 1,
                    ItemImageUrl = "https://www.test.co.uk/picture",
                    ItemName = "Test Name",
                    CurrentPrice = 3,
                    Weight = 5
                }, new ItemAddDTO
                {
                    StoreId = 1,
                    QualityRatingId = 1,
                    ItemImageUrl = "https://www.test.co.uk/picture2",
                    ItemName = "Test Name2",
                    CurrentPrice = 3,
                    Weight = 5
                }, new ItemAddDTO
                {
                    StoreId = 1,
                    QualityRatingId = 1,
                    ItemImageUrl = "https://www.test.co.uk/picture3",
                    ItemName = "Test Name3",
                    CurrentPrice = 3,
                    Weight = 5
                }, new ItemAddDTO
                {
                    StoreId = 1,
                    QualityRatingId = 1,
                    ItemImageUrl = "https://www.test.co.uk/picture4",
                    ItemName = "Test Name4",
                    CurrentPrice = 3,
                    Weight = 5
                }
            };

            // act
            var act = await itemDAL.AddList(itemAddVms);

            // assert
            Assert.Equal(new List<int> { 1, 2, 3, 4 }, act);
        }
    }
}
