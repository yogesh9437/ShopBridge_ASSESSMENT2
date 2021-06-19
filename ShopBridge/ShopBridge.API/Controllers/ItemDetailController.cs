using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBridgeAPI.Models;

namespace ShopBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDetailController : ControllerBase
    {
        private readonly ItemDetailsContext _context;

        public ItemDetailController(ItemDetailsContext context)
        {
            _context = context;
        }

        // GET: api/ItemDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDetails>>> GetItemDetails()
        {
            return await _context.ItemDetails.ToListAsync();
        }

        // GET: api/ItemDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDetails>> GetItemDetails(int id)
        {
            var itemDetails = await _context.ItemDetails.FindAsync(id);

            if (itemDetails == null)
            {
                return NotFound();
            }

            return itemDetails;
        }

        // PUT: api/ItemDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemDetails(int id, ItemDetails itemDetails)
        {
            if (id != itemDetails.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(itemDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetItemDetails", new { id = itemDetails.ItemId }, itemDetails);
        }

        // POST: api/ItemDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemDetails>> PostItemDetails(ItemDetails itemDetails)
        {
            _context.ItemDetails.Add(itemDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemDetails", new { id = itemDetails.ItemId }, itemDetails);
        }

        // DELETE: api/ItemDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemDetails(int id)
        {
            var itemDetails = await _context.ItemDetails.FindAsync(id);
            if (itemDetails == null)
            {
                return NotFound();
            }

            _context.ItemDetails.Remove(itemDetails);
            await _context.SaveChangesAsync();

            return Ok("Record Deleted Successfully.");
        }

        private bool ItemDetailsExists(int id)
        {
            return _context.ItemDetails.Any(e => e.ItemId == id);
        }
    }
}
