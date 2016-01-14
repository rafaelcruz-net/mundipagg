using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain
{
    public class Event
    {
        public virtual Guid Id { get; set; }
        public virtual string Picture {get ;set;}
        public virtual String Name { get; set; }
        public virtual Double Price { get; set; }

    }
}
