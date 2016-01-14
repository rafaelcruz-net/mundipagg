using AutoMapper;
using MundiPagg.Domain;
using MundiPagg.Domain.Service.Interfaces;
using MundiPagg.Web.ModelView;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MundiPagg.Web.Controllers
{
    [Authorize]
    [RoutePrefix("Ticket")]
    public class TicketController : Base.BaseController
    {
        [Inject]
        public IEventService eventService
        {
            get;
            set;
        }

        // GET: Ticket
        [Route("{ticketId}")]
        public ActionResult Index(Guid ticketId)
        {
            var eventDomain = this.eventService.GetById(ticketId);
            var eventModelView = Mapper.Map<Event, EventModelView>(eventDomain);
            TicketModelView ticket = new TicketModelView()
            {
                Event = eventModelView
            };

            return View(ticket);
        }
    }
}