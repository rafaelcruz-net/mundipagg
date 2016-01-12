using MundiPagg.Domain;
using MundiPagg.Repository.Context;
using MundiPagg.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Repository
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {


        public IEnumerable<City> GetCityByState(string uf)
        {
            return (from x in this.DbSet
                    where x.State.UF == uf
                    select x);
        }

    }
}
