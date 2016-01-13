using AutoMapper;
using MundiPagg.Domain;
using MundiPagg.Web.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MundiPagg.Web.App_Start
{
    public class DomainToModelViewMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<CustomerAddress, CreateCustomerAddressModelView>();
            Mapper.CreateMap<Customer, CreateCustomerModelView>();
        }

    }
}