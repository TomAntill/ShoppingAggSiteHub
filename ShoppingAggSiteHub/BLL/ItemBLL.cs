using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingAggSite.BLL.Contracts;
using ShoppingAggSite.DAL.Contracts;
using ShoppingAggSiteHub.DTO.Item;

namespace ShoppingAggSite.BLL
{
    public class ItemBLL : IItemBLL
    {
        private IItemDAL _itemDAL = null;
        public ItemBLL(IItemDAL itemDAL)
        {
            _itemDAL = itemDAL ?? throw new ArgumentNullException(nameof(itemDAL));
        }

        public async Task<int> Add(ItemAddDTO itemAddVm) => await _itemDAL.Add(itemAddVm);

        public async Task<bool> Delete(int id) => await _itemDAL.Delete(id);
        public async Task<bool> Update(ItemUpdateDTO itemUpdateVm) => await _itemDAL.Update(itemUpdateVm);

        public async Task<List<ItemDTO>> GetAllAsync() => await _itemDAL.GetAll();

        public async Task<ItemDTO> GetByIdAsync(int id) => await _itemDAL.GetById(id);

        public Task<List<int>> AddList(List<ItemAddDTO> itemAddVms) => _itemDAL.AddList(itemAddVms);
    }

}
