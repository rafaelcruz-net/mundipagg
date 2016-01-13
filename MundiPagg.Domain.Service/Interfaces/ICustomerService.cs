using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundiPagg.Domain.Service.Interfaces
{
    public interface ICustomerService
    {
        void CreateCustomer(Customer customer);
        void LogOut();
        bool Login(string email, string password, bool rememberMe, bool needsEncryptPassword = true);
        Customer GetCustomerByEmailAndPassword(string username, string password);
    }
}
