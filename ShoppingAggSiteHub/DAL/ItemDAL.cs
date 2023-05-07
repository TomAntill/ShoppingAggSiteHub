using ShoppingAggSite.DAL.Contracts;
using ShoppingAggSite.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAggSiteHub.Guards;
using Microsoft.EntityFrameworkCore;
using ShoppingAggSiteHub.DTO.Item;

namespace ShoppingAggSite.DAL
{
    public class ItemDAL : IItemDAL
    {
        private ShoppingContext _shoppingContext = null;

        public ItemDAL(ShoppingContext shoppingContext)
        {
            _shoppingContext = shoppingContext ?? throw new ArgumentNullException(nameof(shoppingContext));
        }

        //CUDs
        public async Task<int> Add(ItemAddDTO itemAddVm)
        {
            Guard.CheckUrlIsValid(itemAddVm.ItemImageUrl);
            Guard.DecimalIsNotNegative(itemAddVm.CurrentPrice);
            Guard.DecimalIsNotNegative(itemAddVm.Weight);
            Guard.PriceToTwoDecimalPlaces(itemAddVm.CurrentPrice);

            Item item = new Item()
            {
                StoreId = itemAddVm.StoreId,
                QualityRatingId = itemAddVm.QualityRatingId,
                ItemName = itemAddVm.ItemName,
                ItemImageUrl = itemAddVm.ItemImageUrl,
                CurrentPrice = itemAddVm.CurrentPrice,
                Weight = itemAddVm.Weight,
            };

            _shoppingContext.Item.Add(item);
            await _shoppingContext.SaveChangesAsync();
            return item.Id;
        }

        public async Task<bool> Delete(int id)
        {
            Item entity = _shoppingContext.Item.FirstOrDefault(x => x.Id == id);

            Guard.EntityIsNotNull<Item>(entity, id);

            _shoppingContext.Item.Remove(entity);
            int deleted = await _shoppingContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> Update(ItemUpdateDTO itemUpdateVm)
        {
            Guard.CheckUrlIsValid(itemUpdateVm.ItemImageUrl);
            Guard.DecimalIsNotNegative(itemUpdateVm.CurrentPrice);
            Guard.DecimalIsNotNegative(itemUpdateVm.Weight);
            Guard.PriceToTwoDecimalPlaces(itemUpdateVm.CurrentPrice);

            Item entity = _shoppingContext.Item.FirstOrDefault(x => x.Id == itemUpdateVm.ItemId);
            Guard.EntityIsNotNull<Item>(entity, itemUpdateVm.ItemId);

            entity.StoreId = itemUpdateVm.StoreId;
            entity.QualityRatingId = itemUpdateVm.QualityRatingId;
            entity.ItemName = itemUpdateVm.ItemName;
            entity.ItemImageUrl = itemUpdateVm.ItemImageUrl;
            entity.CurrentPrice = itemUpdateVm.CurrentPrice;
            entity.Weight = itemUpdateVm.Weight;

            int updated = await _shoppingContext.SaveChangesAsync();
            return updated > 0;
        }

        // Reads
        public async Task<ItemDTO> GetById(int id)
        {
            Item entity = await _shoppingContext.Item.FirstOrDefaultAsync(x => x.Id == id);

            Guard.EntityIsNotNull<Item>(entity, id);

            ItemDTO itemVm = new ItemDTO()
            {
                Id = entity.Id,
                StoreId = entity.StoreId,
                QualityRatingId = entity.QualityRatingId,
                ItemName = entity.ItemName,
                ItemImageUrl = entity.ItemImageUrl,
                CurrentPrice = entity.CurrentPrice,
                Weight = entity.Weight,
            };
            return itemVm;
        }
        public async Task<List<ItemDTO>> GetAll()
        {
            List<ItemDTO> itemVms = new List<ItemDTO>();
            List<Item> entities = await _shoppingContext.Item.ToListAsync();
            foreach (Item entity in entities)
            {
                ItemDTO itemVm = new ItemDTO()
                {
                    Id = entity.Id,
                    StoreId = entity.StoreId,
                    QualityRatingId = entity.QualityRatingId,
                    ItemName = entity.ItemName,
                    ItemImageUrl = entity.ItemImageUrl,
                    CurrentPrice = entity.CurrentPrice,
                    Weight = entity.Weight,
                };
                itemVms.Add(itemVm);
            }
            return itemVms;
        }

        public async Task<List<int>> AddList(List<ItemAddDTO> itemAddVms)
        {
            List<int> results = new List<int>();

            foreach (ItemAddDTO itemAddVm in itemAddVms)
            {
                Item item = new Item()
                {
                    StoreId = itemAddVm.StoreId,
                    QualityRatingId = itemAddVm.QualityRatingId,
                    ItemName = itemAddVm.ItemName,
                    ItemImageUrl = itemAddVm.ItemImageUrl,
                    CurrentPrice = itemAddVm.CurrentPrice,
                    Weight = itemAddVm.Weight,
                };
                _shoppingContext.Item.Add(item);
                await _shoppingContext.SaveChangesAsync();
                results.Add(item.Id);
            }
            return results;
        }
    }
}
