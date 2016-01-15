using AutoMapper;
using MundiPagg.Domain;
using MundiPagg.Domain.Service.Interfaces;
using MundiPagg.Web.Controllers.Filters;
using MundiPagg.Web.ModelView;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MundiPagg.Web.Controllers
{
    public class AccountController : Base.BaseController
    {
        [Inject]
        public IStateService stateService
        {
            get;
            set;
        }

        [Inject]
        public ICityService cityService
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

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AntiForgeryTokenForAjax]
        public ActionResult Save(CreateCustomerModelView model)
        {
            try
            {
                Customer customer = model.ToDomain();

                var cityId = customer.Address.FirstOrDefault().CityId;
                customer.Address.FirstOrDefault().City = this.cityService.GetCityById(cityId);

                this.customerService.CreateCustomer(customer);
                return JsonSuccess();
            }
            catch (System.Exception ex)
            {
                return JsonError(ex.Message);
            }
        }

        [HttpPost]
        [AntiForgeryTokenForAjax]
        public ActionResult Login(LoginModelView model)
        {
            try
            {
                var result = this.customerService.Login(model.Email, model.Password, true);
                return JsonSuccess<Boolean>(result);
            }
            catch (System.Exception ex)
            {
                return JsonError(ex.Message);
            }
        }


        public ActionResult LogOut()
        {
            this.customerService.LogOut();
            return RedirectToAction("Index", "Home");
        }


        
    }
}