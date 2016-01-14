using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain
{
    public class Customer
    {
        public virtual Guid Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String CPF { get; set; }
        public virtual DateTime Birthday { get; set; }
        public virtual String Genre { get; set; }
        public virtual String Email { get; set; }
        public virtual String Password { get; set; }
        public virtual IList<CustomerAddress> Address { get; set; } = new List<CustomerAddress>();
        public virtual IList<CustomerTicket> Tickets { get; set; } = new List<CustomerTicket>();

    }
}
