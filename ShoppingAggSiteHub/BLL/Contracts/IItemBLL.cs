using ShoppingAggSiteHub.DTO.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAggSite.BLL.Contracts
{
    public interface IItemBLL
    {
        public Task <int> Add(ItemAddDTO itemAddVm);
        public Task<bool> Update(ItemUpdateDTO itemUpdateVm);
        public Task<bool> Delete(int id);
        public Task<ItemDTO> GetByIdAsync(int id);
        public Task<List<ItemDTO>> GetAllAsync();
        public Task<List<int>> AddList(List<ItemAddDTO> t);

    }
}
