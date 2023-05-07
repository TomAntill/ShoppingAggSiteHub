using ShoppingAggSite.BLL.Contracts;
using ShoppingAggSite.DAL.Contracts;
using ShoppingAggSiteHub.DTO.Store;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingAggSite.BLL
{
    public class StoreBLL : IStoreBLL
    {

        private IStoreDAL _storeDAL = null;
        public StoreBLL(IStoreDAL storeDAL)
        {
            _storeDAL = storeDAL ?? throw new ArgumentNullException(nameof(storeDAL));
        }

        public async Task<int> Add(StoreAddDTO t) => await _storeDAL.Add(t);

        public async Task<bool> Delete(int id) => await _storeDAL.Delete(id);
        public async Task<bool> Update(StoreUpdateDTO t) => await _storeDAL.Update(t);

        public async Task<List<StoreDTO>> GetAll() => await _storeDAL.GetAll();

        public async Task<StoreDTO> GetById(int id) => await _storeDAL.GetById(id);

    }
}
