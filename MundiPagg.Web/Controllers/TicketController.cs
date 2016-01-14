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
using System.Web.Security;
using WebMatrix.WebData;

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

        [Inject]
        public ICustomerService customerService
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

        [Route("Save")]
        [HttpPost]
        public ActionResult Save(TicketModelView model)
        {
            try
            {
                var ticket = Mapper.Map<TicketModelView, CustomerTicket>(model);
                var currentUser = Membership.GetUser();
                var eventModel = this.eventService.GetById(model.EventId);

                var customer = this.customerService.GetCustomerById((Guid)currentUser.ProviderUserKey);

                var customerPayment = new CustomerPayment()
                {
                    CreditCardNumber = model.CreditCardNumber,
                    SecurityCode = model.SecurityCode,
                    HolderName = model.HolderName,
                    Expiration = model.Expiration,
                    CreditCardBrand = (CreditCardBrandEnum)Convert.ToInt32(model.CreditCardBrand)
                };

                ticket.Event = eventModel;

                this.customerService.CreateTicket(ticket, customerPayment, customer);
                return JsonSuccess();
            }
            catch (System.Exception ex)
            {
                return JsonError(ex.Message);
            }
            
        }
        [Route("{customerId}/History")]
        public ActionResult History(String customerId)
        {
            var currentUser = Membership.GetUser();
            var customer = this.customerService.GetCustomerById((Guid)currentUser.ProviderUserKey);


            List<TicketModelView> tickets = new List<TicketModelView>();

            foreach (var ticket in customer.Tickets)
            {
                var ticketModelView = new TicketModelView()
                {
                    Event = new EventModelView()
                    {
                        Id = ticket.Event.Id,
                        Name = ticket.Event.Name,
                        Price = ticket.Event.Price,
                        Picture = ticket.Event.Picture
                    },
                    DtEvent = ticket.DtEvent,
                    Quantity = ticket.Quantity,
                    Status = ticket.Status
                };

                tickets.Add(ticketModelView);
            }

            return View(tickets);
        }
    }
}