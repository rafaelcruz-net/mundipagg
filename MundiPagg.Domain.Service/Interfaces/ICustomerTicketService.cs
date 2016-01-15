using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain.Service.Interfaces
{
    public interface ICustomerTicketService
    {
        bool CreateQuickTicket(CustomerTicket ticket, CustomerPayment payment, Customer customer);
        bool CreateTicket(CustomerTicket ticket, CustomerPayment payment, Customer customer);
        void Update(CustomerTicket ticket);
        CustomerTicket GetTicketById(Guid id);
    }
}
