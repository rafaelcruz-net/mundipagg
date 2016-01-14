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
    public class EventMapping : EntityTypeConfiguration<Event>, IMapping
    {
        public EventMapping()
        {
            this.ToTable("Event");
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasColumnName("EventId");
            this.Property(x => x.Name);
            this.Property(x => x.Picture);
            this.Property(x => x.Price);
        }


    }
}
