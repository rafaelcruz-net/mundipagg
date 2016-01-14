using log4net;
using MundiPagg.Domain.Service.Interfaces;
using MundiPagg.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain.Service
{
    public class CityService : ICityService
    {
        private readonly ICityRepository repository;
        ILog Log = log4net.LogManager.GetLogger("MundiPagg.Log");

        public CityService(ICityRepository _repository)
        {
            this.repository = _repository;
        }

        public City GetCityById(int id)
        {
            return this.repository.GetCityById(id);
        }

    }
}
