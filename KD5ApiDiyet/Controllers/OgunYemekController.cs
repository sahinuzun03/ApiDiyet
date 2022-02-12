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
    public class OgunYemekController : ControllerBase
    {
        private readonly DiyetDBContext _context;
        public OgunYemekController(DiyetDBContext context)
        {
            _context = context;
        }
        // GET: api/<OgunYemekController>
        [HttpGet("{Day}/{Hour}")]
        public IEnumerable<Yiyecek> Get(DateTime Day, OgunListesi Hour)
        {
            Ogun ogun = _context.Meals.Include(m => m.Foods).Where(m => m.Day == Day && m.Hour == Hour).FirstOrDefault();
            List<Yiyecek> liste = ogun.Foods.ToList();
            return liste;
        }

        // GET api/<OgunYemekController>/5
        [HttpGet("{Day}/{Hour}/{id}")]
        public Yiyecek Get(int id, DateTime Day, OgunListesi Hour)
        {
            return _context.Meals.Include(m => m.Foods).Where(m => m.Day == Day && m.Hour == Hour).FirstOrDefault().Foods.Where(f => f.ID == id).FirstOrDefault();
        }

        // POST api/<OgunYemekController>
        [HttpPost("{Day}/{Hour}/{id}")]
        public void Post(DateTime Day,OgunListesi Hour,int id)
        {
            _context.Meals.Where(x => x.Day == Day && x.Hour == Hour).FirstOrDefault().Foods.Add(_context.Foods.Find(id));
            _context.SaveChanges();
        }

        // DELETE api/<OgunYemekController>/5
        [HttpDelete("{Day}/{Hour}/{id}")]
        public void Delete(int id, DateTime Day, OgunListesi Hour)
        {
            _context.Meals.Where(x => x.Day == Day && x.Hour == Hour).FirstOrDefault().Foods.Remove(_context.Foods.Find(id));
            _context.SaveChanges();
        }
    }
}
