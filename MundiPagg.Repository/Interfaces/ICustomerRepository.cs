using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MundiPagg.Domain;

namespace MundiPagg.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Customer GetCustomerByCPF(string CPF);
        void Save(Customer customer);
        Customer GetCustomerByEmailAndPassword(string email, string password);

    }
}
