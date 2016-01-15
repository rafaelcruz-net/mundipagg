using MundiPagg.Domain.Enum;
using System;

namespace MundiPagg.Web.ModelView
{
    public class PaymentTokenModelView
    {
        public String CreditCardNumber { get; set; }
        public String SecurityCode { get; set; }
        public CreditCardBrandEnum CreditCardBrand { get; set; }
        public Guid InstantBuy { get; set; }
    }
}