﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain
{
    public class State
    {
        public virtual int Id { get; set; }
        public virtual string UF { get; set; }
        public virtual String Name { get; set; }
    }
}
