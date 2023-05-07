using Microsoft.EntityFrameworkCore;
using ShoppingAggSite.DAL.Contracts;
using ShoppingAggSite.DataModels;
using ShoppingAggSiteHub.DTO.Store;
using ShoppingAggSiteHub.Guards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAggSite.DAL
{
    public class StoreDAL : IStoreDAL
    {

        private ShoppingContext _shoppingContext = null;

        public StoreDAL(ShoppingContext shoppingContext)
        {
            _shoppingContext = shoppingContext ?? throw new ArgumentNullException(nameof(shoppingContext));
        }

        // CUDs
        public async Task<int> Add(StoreAddDTO storeAddVm)
        {
            Guard.CheckUrlIsValid(storeAddVm.StoreImageUrl);

            Store store = new Store()
            {
                BrandId = storeAddVm.BrandId,
                LocationId = storeAddVm.LocationId,
                StoreImageUrl = storeAddVm.StoreImageUrl,
                StoreName = storeAddVm.StoreName
            };


            _shoppingContext.Store.Add(store);
            await _shoppingContext.SaveChangesAsync();
            return store.Id;
        }

        public async Task<bool> Update(StoreUpdateDTO storeUpdateVm)
        {
            Guard.CheckUrlIsValid(storeUpdateVm.StoreImageUrl);

            Store entity = _shoppingContext.Store.FirstOrDefault(x => x.Id == storeUpdateVm.StoreId);
            Guard.EntityIsNotNull<Store>(entity, storeUpdateVm.StoreId);

            entity.BrandId = storeUpdateVm.BrandId;
            entity.LocationId = storeUpdateVm.LocationId;
            entity.StoreImageUrl = storeUpdateVm.StoreImageUrl;
            entity.StoreName = storeUpdateVm.StoreName;

            int updated = await _shoppingContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> Delete(int id)
        {
            Store entity = _shoppingContext.Store.FirstOrDefault(x => x.Id == id);

            Guard.EntityIsNotNull<Store>(entity, id);

            entity.Deleted = true;

            int deleted = await _shoppingContext.SaveChangesAsync();
            return deleted > 0;
        }

        // Reads
        public async Task<StoreDTO> GetById(int id)
        {
            Store entity = await _shoppingContext.Store.FirstOrDefaultAsync(x => x.Id == id);

            Guard.EntityIsNotNull<Store>(entity, id);

            StoreDTO storeVm = new StoreDTO()
            {
                Id = entity.Id,
                BrandId = entity.BrandId,
                LocationId = entity.LocationId,
                StoreImageUrl = entity.StoreImageUrl,
                StoreName = entity.StoreName,
                Deleted = entity.Deleted
            };
            return storeVm;
        }

        public async Task<List<StoreDTO>> GetAll()
        {
            List<StoreDTO> storeVms = new List<StoreDTO>();
            List<Store> entities = await _shoppingContext.Store.ToListAsync();
            foreach (Store entity in entities)
            {
                StoreDTO storeVm = new StoreDTO()
                {
                    Id = entity.Id,
                    BrandId = entity.BrandId,
                    LocationId = entity.LocationId,
                    StoreImageUrl = entity.StoreImageUrl,
                    StoreName = entity.StoreName,
                    Deleted = entity.Deleted
                };
                storeVms.Add(storeVm);
            }
            return storeVms;
        }
    }
}
