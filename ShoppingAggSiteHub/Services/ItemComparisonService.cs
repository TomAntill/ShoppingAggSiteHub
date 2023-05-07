using ShoppingAggSite.BLL.Contracts;
using ShoppingAggSite.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAggSite.Services
{
    public class ItemComparisonService : IItemComparisonService
    {
        private IItemBLL _itemBLL = null;

        public ItemComparisonService(IItemBLL itemBLL)
        {
            _itemBLL = itemBLL ?? throw new ArgumentNullException(nameof(itemBLL));
        }
    }
}
