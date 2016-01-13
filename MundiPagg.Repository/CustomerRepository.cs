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
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public Customer GetCustomerByCPF(string CPF)
        {
            return (from x in this.DbSet
                    where x.CPF == CPF
                    select x).FirstOrDefault();
        }

        public Customer GetCustomerByEmailAndPassword(string email, string password)
        {
            return (from x in this.DbSet
                    where x.Email == email && x.Password == password
                    select x).FirstOrDefault();
        }

        public new void Save(Customer customer)
        {
            base.Save(customer);
        }
    }
}
