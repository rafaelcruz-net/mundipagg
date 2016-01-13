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
    public class StateMapping : EntityTypeConfiguration<State>, IMapping
    {
        public StateMapping()
        {
            this.ToTable("State");
            this.HasKey(x => x.UF);
            this.Property(x => x.UF).HasColumnName("UF");
            this.Property(x => x.Name).HasMaxLength(200);
            this.Property(x => x.UF).HasMaxLength(2);
            this.Property(x => x.CodIbge);
        }

    }
}
