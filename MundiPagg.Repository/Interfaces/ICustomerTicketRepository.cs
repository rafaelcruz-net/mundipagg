using MundiPagg.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Repository.Interfaces
{
    public interface ICustomerTicketRepository
    {
        int Update(CustomerTicket customerTicket);
        CustomerTicket GetTicketById(Guid id);
    }
}
