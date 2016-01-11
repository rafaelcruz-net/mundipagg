using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain
{
    public class CustomerAddress
    {
        public virtual Guid Id { get; set; }
        public virtual Guid IdCustomer { get; set; }
        public virtual int CityId{ get; set; }
        public virtual String Cep { get; set; }
        public virtual String Address { get; set; }
        public virtual String Complement { get; set; }
        public virtual String Number { get; set; }
        public virtual String Neighbor { get; set; }
        public virtual City City { get; set; }
    }
}
