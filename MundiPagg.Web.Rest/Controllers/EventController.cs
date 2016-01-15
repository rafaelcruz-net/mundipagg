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

namespace MundiPagg.Web.RestService.Controllers
{
    [RoutePrefix("api/event")]
    public class EventController : ApiController
    {
        [Inject]
        public IEventService eventService
        {
            get;
            set;
        }

        [Route("")]
        [AcceptVerbs("GET")]
        public ApiResult<List<Event>> Get()
        {
            try
            {
                var events = this.eventService.GetAll().ToList();
                return new ApiResult<List<Event>>()
                {
                    Data = events,
                    Result = true
                };
            }
            catch (Exception Ex)
            {
                return new ApiResult<List<Event>>()
                {
                    Result = false,
                    Data = null,
                    ErrorMessage = Ex.Message
                };
            }
        }
    }
}
