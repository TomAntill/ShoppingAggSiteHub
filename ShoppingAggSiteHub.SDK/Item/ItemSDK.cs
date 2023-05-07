using ShoppingAggSiteHub.DTO.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAggSiteHub.SDK
{
    public class ItemSDK
    {
        private readonly string host;

        public ItemSDK(string host)
        {
            this.host = host;
        }

        public async Task<List<ItemDTO>> GetAllAsync()
        {
            string path = $"{host}/Item/GetAll";
            return await ShoppingAggSiteHubApiSDK.GetAsync<List<ItemDTO>>(path);
        }

        public async Task<ItemDTO> GetByIdAsync(int id)
        {
            string path = $"{host}/Item/GetById?id={id}";
            return await ShoppingAggSiteHubApiSDK.GetAsync<ItemDTO>(path);
        }

        public async Task<int> AddAsync(ItemAddDTO ItemAddDTO)
        {
            string path = $"{host}/Item/Add";
            return await ShoppingAggSiteHubApiSDK.PostAsync<ItemAddDTO, int>(ItemAddDTO, path);
        }

        public async Task<bool> UpdateAsync(ItemUpdateDTO ItemUpdateDTO)
        {
            string path = $"{host}/Item/Update";
            return await ShoppingAggSiteHubApiSDK.PostAsync<ItemUpdateDTO, bool>(ItemUpdateDTO, path);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string path = $"{host}/Item/Delete?id={id}";
            return await ShoppingAggSiteHubApiSDK.DeleteAsync<bool>(path);
        }
    }
}
