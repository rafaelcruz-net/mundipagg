using GatewayApiClient;
using GatewayApiClient.DataContracts;
using GatewayApiClient.DataContracts.EnumTypes;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain.Service.MundiPagg
{
    public class MundiPaggProxy
    {
        static internal Guid merchantKey;
        static internal Uri hostUri;
        static ILog Log = log4net.LogManager.GetLogger("Transaction.Log");

        static MundiPaggProxy()
        {
            if (ConfigurationManager.AppSettings["GatewayService.MerchantKey"] == null)
                throw new ConfigurationErrorsException("Cannot find AppSetting Key GatewayService.MerchantKey on configuration file for this application");

            if (ConfigurationManager.AppSettings["GatewayService.HostUri"] == null)
                throw new ConfigurationErrorsException("Cannot find AppSetting Key GatewayService.HostUri on configuration file for this application");

            merchantKey = new Guid(ConfigurationManager.AppSettings["GatewayService.MerchantKey"].ToString());
            hostUri = new Uri(ConfigurationManager.AppSettings["GatewayService.HostUri"].ToString());
        }


        internal static bool ProcessPayment(CustomerTicket ticket, CustomerPayment payment)
        {
            var amountTransaction = (long)(ticket.Quantity * ticket.Event.Price);
            var expiration = payment.Expiration.Split('/');
            var expMonth = Convert.ToInt32(expiration.FirstOrDefault());
            var expYear = Convert.ToInt32(expiration.LastOrDefault());

            try
            {
                Log.Info($"Create transaction for ticket {ticket.Id} and for customer {ticket.IdCustomer}");

                // Cria a transação.
                var transaction = new CreditCardTransaction()
                {
                    AmountInCents = amountTransaction,
                    CreditCard = new CreditCard()
                    {
                        CreditCardBrand = ConvertCreditCardBrand(payment),
                        CreditCardNumber = payment.CreditCardNumber,
                        ExpMonth = expMonth,
                        ExpYear = expYear,
                        HolderName = payment.HolderName,
                        SecurityCode = payment.SecurityCode
                    },
                };

                // Cria requisição.
                var createSaleRequest = new CreateSaleRequest()
                {
                    // Adiciona a transação na requisição.
                    CreditCardTransactionCollection = new Collection<CreditCardTransaction>(new CreditCardTransaction[] { transaction }),
                };

                Log.Info($"Make request for transaction for ticket {ticket.Id} and for customer {ticket.IdCustomer}");

                var serviceClient = new GatewayServiceClient(merchantKey, hostUri);

                // Autoriza a transação e recebe a resposta do gateway.
                var httpResponse = serviceClient.Sale.Create(createSaleRequest);

                Log.Info($"Request Status for transaction for ticket {ticket.Id} and for customer {ticket.IdCustomer} with status {httpResponse.HttpStatusCode}");
                if (httpResponse.Response.CreditCardTransactionResultCollection != null)
                {
                    var transactionStatus = httpResponse.Response.CreditCardTransactionResultCollection.FirstOrDefault().CreditCardTransactionStatus;
                    Log.Info($"Transaction Status for ticket {ticket.Id} with status {transactionStatus.ToString()}");

                }


            }
            catch (System.Exception ex)
            {
                Log.Error($"Request Error for transaction for ticket {ticket.Id} and for customer {ticket.IdCustomer}", ex);
                throw ex;
            }
            finally
            {
                Log.Info($"End Request for transaction for ticket {ticket.Id} and for customer {ticket.IdCustomer}");
            }

        }

        private static CreditCardBrandEnum ConvertCreditCardBrand(CustomerPayment payment)
        {
            var enumValues = System.Enum.GetValues(typeof(CreditCardBrandEnum));

            foreach (var value in enumValues)
            {
                if ((int)value == (int)payment.CreditCardBrand)
                    return (CreditCardBrandEnum)value;

            }

            return default(CreditCardBrandEnum);

        }

    }
}
