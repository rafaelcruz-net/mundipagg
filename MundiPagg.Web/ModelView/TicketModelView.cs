using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MundiPagg.Web.ModelView
{
    public class TicketModelView
    {
        public EventModelView Event { get; set; }
        public virtual DateTime DtEvent { get; set; }
        public virtual int Quantity { get; set; }
        public virtual String CreditCardNumber { get; set; }
        public virtual String CreditCardBrand { get; set; }
        public virtual String Expiration { get; set; }
        public virtual String SecurityCode { get; set; }
        public virtual String HolderName { get; set; }
    }
}