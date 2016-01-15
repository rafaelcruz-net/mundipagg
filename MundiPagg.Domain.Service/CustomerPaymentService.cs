using log4net;
using MundiPagg.Domain.Enum;
using MundiPagg.Domain.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain.Service
{
    public class CustomerPaymentService : ICustomerPaymentService
    {
        ILog Log = log4net.LogManager.GetLogger("MundiPagg.Log");

       
        public CustomerPayment GetCustomerPaymentByToken(CustomerPaymentTokenizer tokenizer)
        {
            try
            {
                Log.Info($"Consultando o cartões de credito com o token {tokenizer.Id} para o usuário {tokenizer.IdCustomer}");

                var creditCard = MundiPagg.MundiPaggProxy.GetCreditCardByToken(tokenizer);

                if (creditCard == null)
                    return null;

                CustomerPayment payment = new CustomerPayment();
                payment.CreditCardNumber = creditCard.MaskedCreditCardNumber;
                payment.CreditCardBrand = (CreditCardBrandEnum)((int)creditCard.CreditCardBrand);
                payment.InstantBuy = creditCard.InstantBuyKey;
                payment.SecurityCode = tokenizer.SecurityCode;

                return payment;
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                Log.Info($"Finalizando a consulta o cartões de credito com o token {tokenizer.Id} para o usuário {tokenizer.IdCustomer}");
            }

        }

    }
}
