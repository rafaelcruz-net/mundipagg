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
    public class HomeController : Base.BaseController
    {
        [Inject]
        public IEventService eventService
        {
            get;
            set;
        }

        public ActionResult Index()
        {
            var events = this.eventService.GetAll();

            var eventViewModel = events.Select(x => Mapper.Map<Event, EventModelView>(x));

            return View(eventViewModel);
        }

    }
}