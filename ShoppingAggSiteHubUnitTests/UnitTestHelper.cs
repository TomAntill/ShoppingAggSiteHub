using Microsoft.EntityFrameworkCore;
using ShoppingAggSite.DataModels;
using System.Threading.Tasks;

namespace ShoppingAggSiteUnitTests
{
    public class UnitTestHelper
    {

        public UnitTestHelper()
        {  
        }


        public ShoppingContext CreateContextForTest(string contextName)
        {
            var builder = new DbContextOptionsBuilder<ShoppingContext>();
            builder.UseInMemoryDatabase(databaseName: contextName);
            var dbContextOptions = builder.Options;

            ShoppingContext shoppingContext = new ShoppingContext(dbContextOptions);

            shoppingContext.Database.EnsureDeleted();
            shoppingContext.Database.EnsureCreated();

            return shoppingContext;
        }


        public async Task<int> AddItemToContext(ShoppingContext shoppingContext)
        {
            var item = new Item
            {
                StoreId = 1,
                QualityRatingId = 1,
                ItemImageUrl = "https://www.test.co.uk/picture",
                ItemName = "Test Name",
                CurrentPrice = 3,
                Weight = 5
            };
            shoppingContext.Item.Add(item);
            await shoppingContext.SaveChangesAsync();
            return item.Id;
        }

        public async Task<int> AddStoreToContext(ShoppingContext shoppingContext)
        {
            Store store = new Store
            {
                BrandId = 10,
                LocationId = 1,
                StoreImageUrl = "http://www.test.co.uk/picture",
                StoreName = "Test Name"
            };
            shoppingContext.Store.Add(store);
            await shoppingContext.SaveChangesAsync();
            return store.Id;
        }

    }
}
