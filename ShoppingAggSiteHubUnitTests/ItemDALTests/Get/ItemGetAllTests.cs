using ShoppingAggSite.DAL;
using ShoppingAggSite.DataModels;
using ShoppingAggSiteHub.DTO.Item;
using ShoppingAggSiteUnitTests;
using System.Collections.Generic;
using Xunit;

namespace ShoppingAggSiteHubUnitTests.ItemDALTests.GetById
{
    public class ItemGetAllTests
    {
        private readonly UnitTestHelper _unitTestHelper = null;

        public ItemGetAllTests()
        {
            _unitTestHelper = new UnitTestHelper();
        }
        [Fact]
        public async void Item_GetAll_Success_HappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_GetAll_Success_HappyPath");
            await _unitTestHelper.AddItemToContext(contextForTest);
            await _unitTestHelper.AddItemToContext(contextForTest);
            ItemDAL itemDAL = new ItemDAL(contextForTest);

            // act
            List<ItemDTO> act = await itemDAL.GetAll();

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
