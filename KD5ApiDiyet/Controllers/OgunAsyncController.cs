using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KD5ApiDiyet.Database;
using Kd5DiyetModel;

namespace KD5ApiDiyet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgunAsyncController : ControllerBase
    {
        private readonly DiyetDBContext _context;

        public OgunAsyncController(DiyetDBContext context)
        {
            _context = context;
        }

        // GET: api/OgunAsycn
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ogun>>> GetMeals()
        {
            return await _context.Meals.Include(m => m.Foods).ToListAsync();
        }

        // GET: api/OgunAsycn/5
        [HttpGet("{Day}/{Hour}")]
        public async Task<ActionResult<Ogun>> GetOgun(DateTime Day, OgunListesi Hour)
        {
            var ogun = await _context.Meals.Where(x => x.Day == Day && x.Hour == Hour).Include(m => m.Foods).FirstOrDefaultAsync();

            if (ogun == null)
            {
                return NotFound();
            }

            return ogun;
        }

        

        // POST: api/OgunAsycn
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ogun>> PostOgun(Ogun ogun)
        {
            _context.Meals.Add(ogun);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OgunExists(ogun.Day,ogun.Hour))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOgun", new { day = ogun.Day,hour = ogun.Hour }, ogun);
            //Eklediğimiz öğünün b
        }

        // DELETE: api/OgunAsycn/5
        [HttpDelete("{Day}/{Hour}")]
        public async Task<IActionResult> DeleteOgun(DateTime Day, OgunListesi Hour)
        {
            var ogun = await _context.Meals.Where(x => x.Day == Day && x.Hour == Hour).FirstOrDefaultAsync();
            if (ogun == null)
            {
                return NotFound();
            }

            _context.Meals.Remove(ogun);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OgunExists(DateTime Day, OgunListesi Hour)
        {
            return _context.Meals.Any(e => e.Day == Day && e.Hour == Hour);
        }
    }
}
