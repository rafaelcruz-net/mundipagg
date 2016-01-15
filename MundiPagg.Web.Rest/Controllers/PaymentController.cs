using MundiPagg.Domain;
using MundiPagg.Domain.Service.Interfaces;
using MundiPagg.Web.Rest.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MundiPagg.Web.Rest.Controllers
{
    [RoutePrefix("api/payment/{customerId}")]
    public class PaymentController : ApiController
    {
        [Inject]
        public ICustomerPaymentService customerPaymentService
        {
            get;
            set;
        }

        [Inject]
        public ICustomerService customerService
        {
            get;
            set;
        }

        [Route("Saved-Card")]
        [AcceptVerbs("POST")]
        public ApiResult<List<CustomerPayment>> SavedCard(string customerId)
        {
            try
            {
                var customer = this.customerService.GetCustomerById(new Guid(customerId));

                List<CustomerPayment> paymentTokens = new List<CustomerPayment>();

                if (customer.PaymentTokenizer != null && customer.PaymentTokenizer.Count > 0)
                {
                    foreach (var paymentToken in customer.PaymentTokenizer)
                    {
                        var payment = this.customerPaymentService.GetCustomerPaymentByToken(paymentToken);

                        if (payment != null)
                        {
                            paymentTokens.Add(new CustomerPayment()
                            {
                                CreditCardBrand = payment.CreditCardBrand,
                                CreditCardNumber = payment.CreditCardNumber,
                                InstantBuy = payment.InstantBuy,
                                SecurityCode = payment.SecurityCode
                            });
                        }
                    }
                }

                return new ApiResult<List<CustomerPayment>>()
                {
                    Result = true,
                    Data = paymentTokens
                };
            }
            catch (System.Exception ex)
            {
                return new ApiResult<List<CustomerPayment>>()
                {
                    Result = true,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
