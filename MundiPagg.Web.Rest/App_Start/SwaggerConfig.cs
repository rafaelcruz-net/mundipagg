using System.Web.Http;
using WebActivatorEx;
using MundiPagg.Web.Rest;
using Swashbuckle.Application;

namespace MundiPagg.Web.Rest
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "MundiPagg API")
                    .Description("MundiPagg Api");
                })
                .EnableSwaggerUi(c =>
                {

                });
        }
    }
}
