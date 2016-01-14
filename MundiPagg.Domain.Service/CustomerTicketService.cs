using log4net;
using MundiPagg.Domain.Service.MundiPagg;
using MundiPagg.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain.Service
{
    public class CustomerTicketService
    {
        private readonly ICustomerRepository customerRepository;
        ILog Log = log4net.LogManager.GetLogger("MundiPagg.Log");

        private MundiPaggProxy MundiPaggProxy { get; set; }

        public CustomerTicketService()
        {
            this.MundiPaggProxy = new MundiPaggProxy(); 
        }

        public bool CreateTicket(CustomerTicket ticket, CustomerPayment payment, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
