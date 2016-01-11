using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Net;

namespace MundiPagg.Infra.MVC
{
    public class StandardJsonResult : JsonResult
    {
        const string RequestRefused = "This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.";

        public IList<string> ErrorMessages { get; private set; }
        public HttpStatusCode Status { get; set; }


        public StandardJsonResult() 
        {
            ErrorMessages = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }
     

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException(RequestRefused);

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            SerializeData(response);

        }

        protected virtual void SerializeData(HttpResponseBase response)
        {
            var originalData = Data;

            if (ErrorMessages.Any())
            {
                Data = new
                {
                    Success = false,
                    Content = originalData,
                    ErrorMessages = ErrorMessages.ToArray(),
                    RequestTime = DateTime.Now,
                };

                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {

                Data = new
                {
                    Success = true,
                    Content = originalData,
                    ErrorMessages = String.Empty,
                    RequestTime = DateTime.Now,
                };

                response.StatusCode = (int)HttpStatusCode.OK;
            }

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new MundiPagg.Infra.Utils.CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };

            response.Write(JsonConvert.SerializeObject(Data, settings));
        }
    }

    public class StandardJsonResult<T> : StandardJsonResult
    {
        public new T Data
        {
            get { return (T)base.Data; }
            set { base.Data = value; }
        }
    }
}
