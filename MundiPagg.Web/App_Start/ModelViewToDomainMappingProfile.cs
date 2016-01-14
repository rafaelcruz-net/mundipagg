using AutoMapper;
using MundiPagg.Domain;
using MundiPagg.Web.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MundiPagg.Web.App_Start
{
    public class ModelViewToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<CreateCustomerAddressModelView, CustomerAddress>();
            Mapper.CreateMap<CreateCustomerModelView, Customer>();
            Mapper.CreateMap<EventModelView, Event>();
            Mapper.CreateMap<TicketModelView, CustomerTicket>();

        }
    }
}