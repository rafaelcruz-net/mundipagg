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
            this.Property(x => x.Id).HasColumnName("CustomerAdressId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.Name).IsRequired().HasMaxLength(200);
            
            this.HasRequired(x => x.State).WithRequiredPrincipal().Map((x) =>
            {
                x.MapKey("StateId");
            });
        }
    }
}
