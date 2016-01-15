﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain.Service.Interfaces
{
    public interface ICustomerPaymentService
    {
        CustomerPayment GetCustomerPaymentByToken(CustomerPaymentTokenizer tokenizer);
    }
}
