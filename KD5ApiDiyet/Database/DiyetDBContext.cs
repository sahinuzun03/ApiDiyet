using KD5ApiDiyet.Database.Mapping;
using Kd5DiyetModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KD5ApiDiyet.Database
{
    public class DiyetDBContext:DbContext
    {
        public DiyetDBContext(DbContextOptions<DiyetDBContext> options):base(options)
        {  
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration
                ( new OgunMapping())
                .ApplyConfiguration( new YiyecekMapping());
        }
        public DbSet<Yiyecek> Foods { get; set; }
        public DbSet<Ogun> Meals { get; set; }


    }
}
