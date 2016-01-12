using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundiPagg.Repository.Context;
using MundiPagg.Repository.Interfaces;
using MundiPagg.Domain;

namespace MundiPagg.Repository
{
    public class StateRepository : RepositoryBase<State>, IStateRepository
    {
        public new List<State> GetAll()
        {
            return base.GetAll();
        }

    }
}
