using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MundiPagg.Domain.Enum;

namespace MundiPagg.Web.ModelView
{
    public class TicketModelView
    {
        public EventModelView Event { get; set; }
        public Guid EventId { get; set; }
        public DateTime DtEvent { get; set; }
        public int Quantity { get; set; }
        public String CreditCardNumber { get; set; }
        public String CreditCardBrand { get; set; }
        public String Expiration { get; set; }
        public String SecurityCode { get; set; }
        public String HolderName { get; set; }
        public StatusEnum Status { get; internal set; }
    }
}