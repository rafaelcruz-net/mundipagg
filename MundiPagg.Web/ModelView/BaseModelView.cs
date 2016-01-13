using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MundiPagg.Web.ModelView
{
    public abstract class BaseModelView<T>
    {
        public abstract T ToDomain();
    }
}