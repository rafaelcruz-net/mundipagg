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
    public class StateService : IStateService
    {
        private readonly IStateRepository stateRepository;
        private readonly ICityRepository cityRepository;
        ILog Log = log4net.LogManager.GetLogger("MundiPagg.Log");

        public StateService(IStateRepository _repository, ICityRepository _repositoryCity)
        {
            this.stateRepository = _repository;
            this.cityRepository = _repositoryCity;
        }

        public List<State> GetAll()
        {
            return this.stateRepository.GetAll();
        }

        public IEnumerable<City> GetCityByState(string uf)
        {
            return this.cityRepository.GetCityByState(uf);
        }
    }
}
