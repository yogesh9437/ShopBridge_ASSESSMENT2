using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeAPI.Models
{
    public class ItemDetails
    {
        [Key]
        public int ItemId { get; set; }

        [Column (TypeName="nvarchar(100)")]
        public string ItemName { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // you can give MM/YY for both ManufactureDate
        [Column(TypeName = "nvarchar(500)")]
        public string description { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string ExpiryDate { get; set; }

    }
}
