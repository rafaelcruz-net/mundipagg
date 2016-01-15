using MundiPagg.Domain;
using MundiPagg.Repository.Context;
using MundiPagg.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Repository
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public Event GetById(Guid ticketId)
        {
            return base.GetById(ticketId);
        }

        public new List<Event> GetAll()
        {
            return base.GetAll().ToList();
        }

    }
}
