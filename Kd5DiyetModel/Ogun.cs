using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kd5DiyetModel
{
    public enum OgunListesi { Kahvaltı, ÖğleYemeği, AkşamYemeği }
    public class Ogun
    {
       
        public DateTime Day { get; set; }
        public OgunListesi Hour { get; set; }
        public ICollection<Yiyecek> Foods { get; set; }
        public Ogun()
        {
            Foods = new List<Yiyecek>();
        }
    }
}
