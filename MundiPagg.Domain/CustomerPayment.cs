﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain
{
    public  class CustomerPayment
    {
        public String CreditCardNumber { get; set; }
        public String Expiration { get; set; }
        public String SecurityCode { get; set; }
        public String HolderName { get; set; }
    }
}
