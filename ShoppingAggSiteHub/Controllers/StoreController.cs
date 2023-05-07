using Microsoft.AspNetCore.Mvc;
using ShoppingAggSite.BLL.Contracts;
using ShoppingAggSiteHub.DTO.Store;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingAggSite.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private IStoreBLL _storeBLL = null;

        public StoreController(IStoreBLL storeBLL)
        {
            _storeBLL = storeBLL ?? throw new ArgumentNullException(nameof(storeBLL));
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<StoreDTO>> GetAll()
        {            
            return await _storeBLL.GetAll();
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<StoreDTO> GetById(int id)
        {
            return await _storeBLL.GetById(id);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<int> Add([FromBody] StoreAddDTO storeAddVm)
        {
            return await _storeBLL.Add(storeAddVm);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<bool> Update([FromBody] StoreUpdateDTO storeUpdateVm)
        {
            return await _storeBLL.Update(storeUpdateVm);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<bool> Delete(int id)
        {
            return await _storeBLL.Delete(id);
        }
    }
}
