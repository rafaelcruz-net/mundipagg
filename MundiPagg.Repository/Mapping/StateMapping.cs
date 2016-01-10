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
            this.HasKey(x => x.Id);
            this.Property(x => x.Id).HasColumnName("CustomerAdressId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.Name).IsRequired().HasMaxLength(200);
            this.Property(x => x.UF).IsRequired().HasMaxLength(2);
        }

    }
}
