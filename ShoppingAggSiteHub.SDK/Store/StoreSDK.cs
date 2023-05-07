using ShoppingAggSiteHub.DTO.Store;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingAggSiteHub.SDK
{
    public class StoreSDK
    {
        private readonly string host;

        public StoreSDK(string host)
        {
            this.host = host;
        }

        public async Task<List<StoreDTO>> GetAllAsync()
        {
            string path = $"{host}/store/GetAll";
            return await ShoppingAggSiteHubApiSDK.GetAsync<List<StoreDTO>>(path);
        }

        public async Task<StoreDTO> GetByIdAsync(int id)
        {
            string path = $"{host}/store/GetById?id={id}";
            return await ShoppingAggSiteHubApiSDK.GetAsync<StoreDTO>(path);
        }

        public async Task<int> AddAsync(StoreAddDTO storeAddDTO)
        {
            string path = $"{host}/store/Add";
            return await ShoppingAggSiteHubApiSDK.PostAsync<StoreAddDTO, int>(storeAddDTO, path);
        }

        public async Task<bool> UpdateAsync(StoreUpdateDTO storeUpdateDTO)
        {
            string path = $"{host}/store/Update";
            return await ShoppingAggSiteHubApiSDK.PostAsync<StoreUpdateDTO, bool>(storeUpdateDTO, path);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string path = $"{host}/store/Delete?id={id}";
            return await ShoppingAggSiteHubApiSDK.DeleteAsync<bool>(path);
        }
    }
}
