using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain
{
    public class CustomerPaymentTokenizer
    {
        public virtual Guid Id
        {
            get;
            set;
        }

        public virtual String Token
        {
            get;
            set;
        }

        public virtual String SecurityCode
        {
            get;
            set;
        }

        public virtual Guid IdCustomer
        {
            get;
            set;
        }
    }
}
