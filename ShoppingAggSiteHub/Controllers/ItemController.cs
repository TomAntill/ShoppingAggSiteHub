using Microsoft.AspNetCore.Mvc;
using ShoppingAggSite.BLL.Contracts;
using ShoppingAggSiteHub.DTO.Item;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingAggSite.Controllers
{
    [Route("api/item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
           
        private IItemBLL _itemBLL = null;

        public ItemController(IItemBLL itemBLL)
        {
            _itemBLL = itemBLL ?? throw new ArgumentNullException(nameof(itemBLL));
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<ItemDTO>> GetAll()
        {            
            return await _itemBLL.GetAllAsync();
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ItemDTO> GetById(int id)
        {
            return await _itemBLL.GetByIdAsync(id);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<int> Add([FromBody] ItemAddDTO itemAddVm)
        {
            return await _itemBLL.Add(itemAddVm);
        }

        [HttpPost]
        [Route("AddList")]
        public async Task<List<int>> AddList([FromBody] List<ItemAddDTO> t)
        {
            return await _itemBLL.AddList(t);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<bool> Update([FromBody] ItemUpdateDTO itemUpdateVm)
        {
            return await _itemBLL.Update(itemUpdateVm);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<bool> Delete(int id)
        {
            return await _itemBLL.Delete(id);
        }
    }
}
