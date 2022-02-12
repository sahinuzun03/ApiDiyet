using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KD5ApiDiyet.Database.Mapping;
using Kd5DiyetModel;
using KD5ApiDiyet.Database;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KD5ApiDiyet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgunController : ControllerBase
    {
        private readonly DiyetDBContext _context;
        public OgunController(DiyetDBContext context)
        {
            _context = context;
        }
        // GET: api/<Ogun>
        [HttpGet]
        public IEnumerable<Ogun> Get()
        {
            return _context.Meals.Include(m => m.Foods).ToList();// Liste olarak döndürecek mealslarda alt tablolarında gelmesini istiyorsak
        }

        // GET api/<Ogun>/5
        [HttpGet("{Day}/{Hour}")]
        public ActionResult<Ogun> Get(DateTime Day,OgunListesi Hour)
        {
            var result = _context.Meals.Where(x => x.Day == Day && x.Hour == Hour).Include(m => m.Foods).FirstOrDefault();
            if (result == null)
                return NotFound();

            return result;
        }

        // POST api/<Ogun>
        [HttpPost]
        public ActionResult Post([FromBody] Ogun value)
        {
            _context.Meals.Add(value);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/<Ogun>/5
        [HttpDelete("{Day}/{Hour}")]
        public ActionResult Delete(DateTime Day, OgunListesi Hour)
        {
            var result = _context.Meals.Where(x => x.Day == Day && x.Hour == Hour).FirstOrDefault();
            if (result == null)
                return NotFound();
            _context.Meals.Remove(result);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return NotFound();
            }
            return NoContent();
        }
    }
}
