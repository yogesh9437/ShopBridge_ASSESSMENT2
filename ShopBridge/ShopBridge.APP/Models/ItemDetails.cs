using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.APP.Models
{
    public class ItemDetails
    {
        public int Itemid { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ExpiryDate { get; set; }
    }
}
