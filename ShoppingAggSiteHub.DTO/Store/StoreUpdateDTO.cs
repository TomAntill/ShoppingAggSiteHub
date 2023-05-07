using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAggSiteHub.DTO.Store
{
    public class StoreUpdateDTO
    {
        public int StoreId { get; set; }
        public int BrandId { get; set; }
        public int LocationId { get; set; }
        public string StoreImageUrl { get; set; }
        public string StoreName { get; set; }
    }
}
