using MundiPagg.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Repository.Interfaces
{
    public interface IEventRepository
    {
        List<Event> GetAll();
        Event GetById(Guid ticketId);
    }
}
