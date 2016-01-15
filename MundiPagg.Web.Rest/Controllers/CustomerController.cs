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
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        [Inject]
        public ICustomerService customerService
        {
            get;
            set;
        }

        [AcceptVerbs("POST")]
        [Route("")]
        public ApiResult<Customer> Save(Customer customer)
        {
            try
            {
                var cityId = customer.Address.FirstOrDefault().CityId;

                this.customerService.CreateCustomer(customer);
                return new ApiResult<Customer>()
                {
                    Result = true,
                    Data = customer
                };

            }
            catch (System.Exception ex)
            {
                return new ApiResult<Customer>()
                {
                    Result = true,
                    Data = null,
                    ErrorMessage = ex.Message
                };
            }
        }

        [Route("Login")]
        [AcceptVerbs("POST")]
        public ApiResult<Customer> Login(string email, string password)
        {
            try
            {
                var hashedPwd = Infra.Utils.SecurityUtils.HashSHA1(password);

                var result = this.customerService.GetCustomerByEmailAndPassword(email, hashedPwd);
                var loginValid = false;

                if (result != null)
                    loginValid = true;

                return new ApiResult<Customer>()
                {
                    Result = loginValid,
                    Data = result
                };
            }
            catch (System.Exception ex)
            {
                return new ApiResult<Customer>()
                {
                    Result = true,
                    Data = null,
                    ErrorMessage = ex.Message
                };
            }
        }

    }
}
