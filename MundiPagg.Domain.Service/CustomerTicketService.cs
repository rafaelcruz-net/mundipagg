using log4net;
using MundiPagg.Domain.Service.Interfaces;
using MundiPagg.Domain.Service.MundiPagg;
using MundiPagg.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain.Service
{
    public class CustomerTicketService : ICustomerTicketService
    {
        private readonly ICustomerRepository customerRepository;
        ILog Log = log4net.LogManager.GetLogger("MundiPagg.Log");

        private MundiPaggProxy MundiPaggProxy { get; set; }

        public CustomerTicketService(ICustomerRepository _repository)
        {
            this.MundiPaggProxy = new MundiPaggProxy();
            this.customerRepository = _repository;
        }

        public bool CreateTicket(CustomerTicket ticket, CustomerPayment payment, Customer customer)
        {
            try
            {
                Dictionary<String, Object> extraParamenter = new Dictionary<string, object>();
                extraParamenter.Add("ticket", JsonConvert.SerializeObject(ticket));
                extraParamenter.Add("payment", JsonConvert.SerializeObject(payment));
                Infra.Queue.AzureQueueMessageManager.Enqueue(customer.Id.ToString(), customer.Name, null, Infra.Queue.AzureMessageType.NEW_TRANSACTION, extraParamenter);
                return true;
            }
            catch(System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
