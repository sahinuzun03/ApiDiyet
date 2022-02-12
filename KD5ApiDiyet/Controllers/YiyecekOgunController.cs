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
    public class YiyecekOgunController : ControllerBase
    {
        private readonly DiyetDBContext _context;
        public YiyecekOgunController(DiyetDBContext context)
        {
            _context = context;
        }
        // GET: api/<YiyecekOgunController>
        [HttpGet("{id}")]
        public IEnumerable<Ogun> Get(int id)
        {
            return _context.Foods.Include(y => y.Meals).Where(y => y.ID == id).FirstOrDefault().Meals.ToList();
        }
        // POST api/<YiyecekOgunController>
        [HttpPost("{id}/{Day}/{Hour}")]
        public IActionResult Post(int id , DateTime Day,OgunListesi hour)
        {
            Yiyecek yemek = _context.Foods.Where(y => y.ID == id).FirstOrDefault();
            if(yemek == null)
                return NotFound();

            Ogun ogun = _context.Meals.Where(o => o.Day == Day && o.Hour == hour).FirstOrDefault();
            if (ogun == null)
                return NotFound();

            yemek.Meals.Add(ogun);
            _context.SaveChanges();
            return Ok();
            
        }

        // PUT api/<YiyecekOgunController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<YiyecekOgunController>/5
        [HttpDelete("{id}/{Day}/{Hour}")]
        public IActionResult Delete(int id, DateTime Day, OgunListesi hour)
        {
            Yiyecek yemek = _context.Foods.Where(y => y.ID == id).FirstOrDefault();
            if (yemek == null)
                return NotFound();

            Ogun ogun = _context.Meals.Where(o => o.Day == Day && o.Hour == hour).FirstOrDefault();
            if (ogun == null)
                return NotFound();

            Ogun yenen = _context.Foods.Include(y => y.Meals).Where(y => y.ID == id).FirstOrDefault().Meals.Where(m => m.Day == Day && m.Hour == hour).FirstOrDefault();

            if(yenen == null)
            {
                return NotFound();
            }

            yemek.Meals.Remove(ogun);
            _context.SaveChanges();
            return Ok();


        }
    }
}
