using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MundiPagg.Web.ModelView
{
    public class EventModelView
    {
        public virtual Guid Id { get; set; }
        public virtual string Picture { get; set; }
        public virtual String Name { get; set; }
        public virtual Double Price { get; set; }
    }
}