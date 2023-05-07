using ShoppingAggSite.DAL;
using ShoppingAggSite.DataModels;
using ShoppingAggSiteHub.Exceptions;
using ShoppingAggSiteUnitTests;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingAggSiteHubUnitTests.Delete
{
    public class ItemDeleteTests
    {
        private readonly UnitTestHelper _unitTestHelper = null;

        public ItemDeleteTests()
        {
            _unitTestHelper = new UnitTestHelper();
        }

        [Fact]
        public async void Item_Delete_Success_HappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Delete_Success_HappyPath");
            int id = await _unitTestHelper.AddItemToContext(contextForTest);

            Item item = contextForTest.Item.Single(x => x.Id == id);
            ItemDAL itemDAL = new ItemDAL(contextForTest);

            // act
            bool success = await itemDAL.Delete(item.Id);

            // assert
            Assert.True(success);
        }      

        [Fact]
        public async void Item_Delete_Failure_NoEntityFound_UnhappyPath()
        {
            // arrange
            ShoppingContext contextForTest = _unitTestHelper.CreateContextForTest("Item_Delete_Failure_NoEntityFound_UnhappyPath");
            ItemDAL itemDAL = new ItemDAL(contextForTest);

            // act
            Func<Task<bool>> act = async () => await itemDAL.Delete(99);

            // assert
            await Assert.ThrowsAsync<EntityNotFoundException>(act);
        }       
    }
}
