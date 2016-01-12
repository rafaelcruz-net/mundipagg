using MundiPagg.Domain;
using MundiPagg.Domain.Service.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MundiPagg.Web.Controllers
{
    [RoutePrefix("State")]
    public class StateController : Base.BaseController
    {
        [Inject]
        public IStateService stateService
        {
            get;
            set;
        }

        // GET: State
        [HttpPost]
        [Route("{uf}/Cities")]
        public ActionResult Index(string uf)
        {
            var cities = this.stateService.GetCityByState(uf).ToList();
            return JsonSuccess<IEnumerable<City>>(cities);
        }
    }
}