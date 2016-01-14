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
        Customer GetCustomerByEmailAndPassword(string email, string password);
        Customer GetCustomerById(Guid customerId);
        Customer GetCustomerByEmail(string username);
        void Save(Customer customer);
        void Update(Customer customer);


    }
}
