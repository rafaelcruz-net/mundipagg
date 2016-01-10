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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        ILog Log = log4net.LogManager.GetLogger("MundiPagg.Log");

        public CustomerService(ICustomerRepository _repository)
        {
            this.customerRepository = _repository;
        }
    }
}
