using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain
{
    public class City
    {
        public virtual int Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String Uf { get; set; }
        public virtual String CodIbge { get; set; }
        public virtual String Area { get; set; }
        public virtual State State { get; set; }
    }
}
