using ShoppingAggSiteHub.DTO.Store;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingAggSite.DAL.Contracts
{
    public interface IStoreDAL
    {
        public Task<int> Add(StoreAddDTO t);

        public Task<bool> Update(StoreUpdateDTO t);

        public Task<bool> Delete(int id);

        public Task<StoreDTO> GetById(int id);

        public Task<List<StoreDTO>> GetAll();
    }
}
