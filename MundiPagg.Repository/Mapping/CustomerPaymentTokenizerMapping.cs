using MundiPagg.Domain;
using MundiPagg.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Repository.Mapping
{
    public class CustomerPaymentTokenizerMapping : EntityTypeConfiguration<CustomerPaymentTokenizer>, IMapping
    {
        public CustomerPaymentTokenizerMapping()
        {
            this.ToTable("CustomerPaymentTokenizer");
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasColumnName("CustomerPaymentTokenizerId");
            this.Property(x => x.Token);
            this.Property(x => x.SecurityCode);
        }

    }
}
