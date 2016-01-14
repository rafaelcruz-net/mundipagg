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
        }

        private void ProcessTransaction(AzureMessage azureMessage)
        {

            var jsonPayment = (string)azureMessage.ExtraData["payment"];
            var jsonTicket = (string)azureMessage.ExtraData["ticket"];

            CustomerPayment payment = JsonConvert.DeserializeObject<CustomerPayment>(jsonPayment);
            CustomerTicket ticket = JsonConvert.DeserializeObject<CustomerTicket>(jsonTicket);



            
        }

        private Stream GetStreamEmail(string key)
        {
            Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(key);
            return stream;
        }

    }
}
