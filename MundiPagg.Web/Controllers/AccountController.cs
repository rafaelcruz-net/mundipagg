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
    [RoutePrefix("Account")]
    public class AccountController : Base.BaseController
    {
        [Inject]
        public IStateService stateService
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

        
        // GET: Login
        public ActionResult Create()
        {

            var states = this.stateService.GetAll();

            ViewBag.States = states.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.UF
            });

            return View();
        }

        [Route("Save")]
        [HttpPost]
        public ActionResult Save(CreateCustomerModelView model)
        {
            try
            {
                Customer customer = model.ToDomain();
                this.customerService.CreateCustomer(customer);
                return JsonSuccess();
            }
            catch (System.Exception ex)
            {
                return JsonError(ex.Message);
            }
        }

        
    }
}