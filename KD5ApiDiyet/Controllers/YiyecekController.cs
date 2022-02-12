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

namespace RoniHoca_KD5ApiDiyet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YiyecekController : ControllerBase
    {
        private readonly DiyetDBContext _context;
        public YiyecekController(DiyetDBContext context)
        {
            _context = context;
        }
        // GET: api/<YiyecekController>
        [HttpGet]
        public IEnumerable<Yiyecek> Get()
        {
            return _context.Foods.Include(f=>f.Meals).ToList(); // Bu yemekler hangi öğünlerde yenilmiş getir demek için include yaptık.Ama Json diye bir dosya yükledik startup'a kod yazdık.
        }

        // GET api/<YiyecekController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Yiyecek> Get(int id)
        {
            var result = _context.Foods.Include(f => f.Meals).ToList().Where(f => f.ID == id).FirstOrDefault();
            if (result == null)
                return NotFound();

            return result;
        }

        // POST api/<YiyecekController>
        [HttpPost]
        public ActionResult Post([FromBody] Yiyecek value)
        {
            _context.Foods.Add(value);
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return NotFound();
            }
            return NoContent();

        }

        // PUT api/<YiyecekController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Yiyecek value)
        {
            if (id != value.ID)
                return BadRequest();
            _context.Entry(value).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {

                NotFound();
            }
            return NoContent();
        }

        // DELETE api/<YiyecekController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var y = _context.Foods.Find(id);
            if (y == null)
                return NotFound();

            _context.Foods.Remove(y);
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
