using log4net;
using MundiPagg.Domain.Service.Interfaces;
using MundiPagg.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository repository;
        ILog Log = log4net.LogManager.GetLogger("MundiPagg.Log");

        public EventService(IEventRepository _repository)
        {
            this.repository = _repository;
        }

        public List<Event> GetAll()
        {
            return this.repository.GetAll();
        }

        public Event GetById(Guid ticketId)
        {
            return this.repository.GetById(ticketId);
        }
    }
}
