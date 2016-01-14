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
    public class CustomerTicketMapping  : EntityTypeConfiguration<CustomerTicket>, IMapping
    {
        public CustomerTicketMapping()
        {
            this.ToTable("CustomerEvent");
            this.HasKey(x => x.Id);
            this.Property(x => x.DtEvent);
            this.Property(x => x.Quantity);
            this.HasRequired(x => x.Event).WithMany().HasForeignKey(x => x.IdEvent);
        }

    }
}

