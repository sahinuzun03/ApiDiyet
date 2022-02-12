using Kd5DiyetModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KD5ApiDiyet.Database.Mapping
{
    public class OgunMapping : IEntityTypeConfiguration<Ogun>
    {
        public void Configure(EntityTypeBuilder<Ogun> builder)
        {
            builder.Property(o => o.Day).HasColumnType<DateTime>("date");

            builder.Property(o => o.Hour).HasColumnType<OgunListesi>("int");
            builder.HasKey(o => new { o.Day, o.Hour });

            //builder.HasMany<Yiyecek>(o => o.Foods).WithMany(y => y.Meals);
        }
    }
}
