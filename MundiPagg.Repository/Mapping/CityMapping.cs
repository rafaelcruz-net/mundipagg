using MundiPagg.Domain;
using MundiPagg.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Repository.Mapping
{
    public class CityMapping : EntityTypeConfiguration<City>, IMapping
    {
        public CityMapping()
        {
            this.ToTable("City");
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasColumnName("CityId");
            this.Property(x => x.Name).HasMaxLength(200);
            this.Property(x => x.Uf).HasMaxLength(200);
            this.Property(x => x.CodIbge).HasMaxLength(200);
            this.Property(x => x.Area).HasMaxLength(200);

            this.HasRequired(x => x.State).WithMany().HasForeignKey(x => x.Uf);
        }
    }
}
