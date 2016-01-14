using MundiPagg.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain
{
    public class CustomerTicket
    {
        public virtual Guid Id { get; set; }
        public virtual Guid IdEvent { get; set; }
        public virtual Event Event { get; set; }
        public virtual DateTime DtEvent { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Guid IdCustomer { get; set; }
        public virtual StatusEnum Status { get; set; } 
    }
}
