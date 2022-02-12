using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kd5DiyetModel
{
    public enum YiyecekTipi { Meyve, Etler, Tahıl, SütÜrünleri }
    public class Yiyecek
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public YiyecekTipi Type { get; set; }
        public int Kalori { get; set; }
        public ICollection<Ogun> Meals { get; set; }
        public Yiyecek()
        {
            Meals = new List<Ogun>();
        }
    }
}
