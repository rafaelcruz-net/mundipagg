using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MundiPagg.Web.Rest.Models
{
    public class ApiResult<T>
    {
        public bool Result { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}