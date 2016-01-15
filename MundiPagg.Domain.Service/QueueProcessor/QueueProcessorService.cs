using Microsoft.ServiceBus.Messaging;
using MundiPagg.Domain.Service.Interfaces;
using MundiPagg.Domain.Service.MundiPagg;
using MundiPagg.Infra.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MundiPagg.Domain.Service.QueueProcessor
{
    public class QueueProcessorService : IQueueProcessorService
    {
        private readonly ICustomerService customerService;
        private readonly ICustomerTicketService customerTicketService;

        private readonly string NewTransactionUpdate= "MundiPagg.Domain.Service.QueueProcessor.TemplateEmail.email-usuario-new-ticket-update.cshtml";

        public QueueProcessorService(ICustomerService customerService, ICustomerTicketService customerTicketService)
        {
            this.customerService = customerService;
            this.customerTicketService = customerTicketService;
        }

        public void ProcessMessage(BrokeredMessage message)
        {
            Infra.Queue.AzureMessage azureMessage = message.GetBody<AzureMessage>();
            ProcessNewMessage(azureMessage);
        }

        private void ProcessNewMessage(Infra.Queue.AzureMessage azureMessage)
        {

            if (azureMessage.MessageType == (int)Infra.Queue.AzureMessageType.NEW_TRANSACTION)
            {
                ProcessTransaction(azureMessage); 
            }

            if (azureMessage.MessageType == (int)Infra.Queue.AzureMessageType.NEW_TRANSACTION_INSTANT_BUY)
            {
                ProcessQuickTransaction(azureMessage);
            }
        }

        private void ProcessQuickTransaction(AzureMessage azureMessage)
        {
            var jsonTicket = (string)azureMessage.ExtraData["ticket"];
            var jsonPayment = (string)azureMessage.ExtraData["payment"];

            CustomerTicket ticket = JsonConvert.DeserializeObject<CustomerTicket>(jsonTicket);
            CustomerPayment payment = JsonConvert.DeserializeObject<CustomerPayment>(jsonPayment);
            Customer customer = this.customerService.GetCustomerById(new Guid(azureMessage.SenderUserId));

            ticket.IdEvent = ticket.Event.Id;
            ticket.IdCustomer = customer.Id;

            if (MundiPaggProxy.ProcessPayment(ticket, payment.InstantBuy, payment.SecurityCode))
            {
                var ticketUpdate = this.customerTicketService.GetTicketById(ticket.Id);
                ticketUpdate.Status = ticket.Status;

                this.customerTicketService.Update(ticketUpdate);
                this.SendEmail(ticket, customer);
            }

        }

        private void ProcessTransaction(AzureMessage azureMessage)
        {

            var jsonPayment = (string)azureMessage.ExtraData["payment"];
            var jsonTicket = (string)azureMessage.ExtraData["ticket"];

            CustomerPayment payment = JsonConvert.DeserializeObject<CustomerPayment>(jsonPayment);
            CustomerTicket ticket = JsonConvert.DeserializeObject<CustomerTicket>(jsonTicket);
            Customer customer = this.customerService.GetCustomerById(new Guid(azureMessage.SenderUserId));

            ticket.IdEvent = ticket.Event.Id;
            ticket.IdCustomer = customer.Id;

            Guid instantBuy;

            if (MundiPaggProxy.ProcessPayment(ticket, payment, out instantBuy))
            {
                var ticketUpdate = this.customerTicketService.GetTicketById(ticket.Id);
                ticketUpdate.Status = ticket.Status;

                this.customerTicketService.Update(ticketUpdate);
                this.SendEmail(ticket, customer);

                if (payment.KeepSave)
                {
                    if (!customer.PaymentTokenizer.Any(x => x.Token == instantBuy.ToString()))
                    {
                        var customerToken = new CustomerPaymentTokenizer()
                        {
                            Id = Guid.NewGuid(),
                            IdCustomer = customer.Id,
                            SecurityCode = payment.SecurityCode,
                            Token = instantBuy.ToString()
                        };

                        customer.PaymentTokenizer.Add(customerToken);
                        this.customerService.Update(customer);
                    }
                }
            }

        }

        private void SendEmail(CustomerTicket ticket, Customer customer)
        {
            var model = new
            {
                Name = customer.Name,
                EventName = ticket.Event.Name,
                Status = ticket.Status.ToString(),
            };
            Stream stream = GetStreamEmail(this.NewTransactionUpdate);
            string emailBody = Infra.Utils.EmailUtils.TransformTemplate(stream, model);

            Infra.Utils.EmailUtils.SendEmail(customer.Email, emailBody, Infra.Utils.EmailUtils.EmaiSubject.NEW_TRANSACTION_UPDATE);
        }

        private Stream GetStreamEmail(string key)
        {
            Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(key);
            return stream;
        }

    }
}
