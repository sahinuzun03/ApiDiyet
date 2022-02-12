using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kd5DiyetModel; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KD5ApiDiyet.Database.Mapping
{
    public class YiyecekMapping : IEntityTypeConfiguration<Yiyecek>
    {
        public void Configure(EntityTypeBuilder<Yiyecek> builder)
        {
            //builder.ToTable("Yiyecek");
            builder.HasKey(x=>x.ID);

            builder.Property(y => y.ID).UseIdentityColumn<int>(1,1);

            builder.Property(y => y.Name).IsUnicode(true).IsRequired().HasMaxLength(50);

            builder.Property(y => y.Type).HasColumnType<YiyecekTipi>("int");
            builder.HasCheckConstraint("CHK_Yiyecek_KolariSifirdanBüyük", "[kalori]>0");

            //builder.Property(y=>y.Kalori)

            builder.HasMany(y => y.Meals).WithMany(x => x.Foods);

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
