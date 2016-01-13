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
    public class CustomerMapping : EntityTypeConfiguration<Customer>, IMapping
    {
        public CustomerMapping()
        {
            this.ToTable("Customer");
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasColumnName("CustomerId");
            this.Property(x => x.Name).IsRequired().HasMaxLength(200);
            this.Property(x => x.CPF).IsRequired().HasMaxLength(11);
            this.Property(x => x.Birthday).IsRequired();
            this.Property(x => x.Genre).IsRequired().HasMaxLength(1);
            this.Property(x => x.Email).IsRequired().HasMaxLength(200);

           // this.HasMany(x => x.Address).WithOptional().HasForeignKey(x => x.IdCustomer);

        }

    }
}
