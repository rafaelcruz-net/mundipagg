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
    public class CustomerAddressMapping : EntityTypeConfiguration<CustomerAddress>, IMapping
    {
        public CustomerAddressMapping()
        {
            this.ToTable("CustomerAddress");
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasColumnName("CustomerAdressId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.Address).IsRequired().HasMaxLength(500);
            this.Property(x => x.Cep).IsRequired().HasMaxLength(9);
            this.Property(x => x.Complement).IsRequired().HasMaxLength(200);
            this.Property(x => x.Number).IsRequired().HasMaxLength(5);
            this.Property(x => x.Neighbor).IsRequired().HasMaxLength(100);
            
            this.HasRequired(x=>x.City).WithRequiredPrincipal().Map((x) =>
            {
                x.MapKey("CityId");
            });




        }


    }
}

