using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeAPI.Models
{
    public class ItemDetailsContext:DbContext
    {
        public ItemDetailsContext(DbContextOptions<ItemDetailsContext> options) : base(options)
        {

        }

        public DbSet<ItemDetails> ItemDetails { get; set; }
    }
}
