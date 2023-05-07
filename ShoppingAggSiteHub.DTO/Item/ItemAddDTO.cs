using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAggSiteHub.DTO.Item
{
    public class ItemAddDTO
    {
        public int StoreId { get; set; }
        public int QualityRatingId { get; set; }
        public string ItemName { get; set; }
        public string ItemImageUrl { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Weight { get; set; }
    }  
}
