using MundiPagg.Domain;
using MundiPagg.Domain.Service.Interfaces;
using MundiPagg.Web.Rest.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MundiPagg.Web.Rest.Controllers
{
    [RoutePrefix("api/ticket")]
    public class TicketController : ApiController
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

        [Inject]
        public ICustomerPaymentService customerPaymentService
        {
            get;
            set;
        }

        [Route("{customerId}")]
        [AcceptVerbs("POST")]
        public ApiResult<CustomerTicket> Save(string customerId, CustomerTicket ticket, CustomerPayment payment, Event eventTicket)
        {
            try
            {
                var customer = this.customerService.GetCustomerById(Guid.Parse(customerId));
                ticket.Event = eventTicket;
                var result = this.customerService.CreateTicket(ticket, payment, customer);

                return new ApiResult<CustomerTicket>()
                {
                    Result = result,
                    Data = ticket,
                };
            }
            catch (System.Exception ex)
            {
                return new ApiResult<CustomerTicket>()
                {
                    Result = false,
                    Data = null,
                    ErrorMessage = ex.Message
                };
            }
        }

        [Route("{customerId}/create-quick")]
        [AcceptVerbs("POST")]
        public ApiResult<CustomerTicket> SaveQuick(string customerId, CustomerTicket ticket, CustomerPayment payment, Event eventTicket)
        {
            try
            {
                var customer = this.customerService.GetCustomerById(Guid.Parse(customerId));
                ticket.Event = eventTicket;
                var result = this.customerService.CreateQuickTicket(ticket, payment, customer);

                return new ApiResult<CustomerTicket>()
                {
                    Result = result,
                    Data = ticket,
                };
            }
            catch (System.Exception ex)
            {
                return new ApiResult<CustomerTicket>()
                {
                    Result = false,
                    Data = null,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
