using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MundiPagg.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMapping()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToModelViewMappingProfile>();
                x.AddProfile<ModelViewToDomainMappingProfile>();
            });
        }
    }
}