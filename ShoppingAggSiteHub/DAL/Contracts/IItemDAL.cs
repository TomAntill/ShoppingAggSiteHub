using ShoppingAggSiteHub.DTO.Item;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingAggSite.DAL.Contracts
{
    public interface IItemDAL
    {
        public Task<int> Add(ItemAddDTO t);

        public Task<bool> Update(ItemUpdateDTO itemUpdateVm);

        public Task<bool> Delete(int id);

        public Task<ItemDTO> GetById(int id);

        public Task<List<ItemDTO>> GetAll();

        public Task<List<int>> AddList(List<ItemAddDTO> t);
    }
}
