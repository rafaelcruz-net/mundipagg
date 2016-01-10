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
            this.Property(x => x.Id).HasColumnName("CustomerId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.Name).HasColumnName("CustomerName").IsRequired().HasMaxLength(200);
            this.Property(x => x.CPF).HasColumnName("CustomerCPF").IsRequired().HasMaxLength(11);
            this.Property(x => x.Birthday).HasColumnName("CustomerBirthday").IsRequired();
            this.Property(x => x.Genre).HasColumnName("CustomerGenre").IsRequired().HasMaxLength(1);

            this.HasMany(x => x.Address).WithOptional().HasForeignKey(x => x.IdCustomer);

        }

    }
}
