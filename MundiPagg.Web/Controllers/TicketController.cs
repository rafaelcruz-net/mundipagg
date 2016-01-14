using AutoMapper;
using MundiPagg.Domain;
using MundiPagg.Domain.Enum;
using MundiPagg.Domain.Service.Interfaces;
using MundiPagg.Infra.Utils;
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

            var enumSource = EnumerationUtils.GetAll<CreditCardBrandEnum>();

            ViewBag.CreditCardBrandEnum = enumSource.Select(x => new SelectListItem
            {
                Text = x.Value,
                Value = x.Key.ToString()
            });

            TicketModelView ticket = new TicketModelView()
            {
                Event = eventModelView
            };

            return View(ticket);
        }
    }
}