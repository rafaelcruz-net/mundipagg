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
        Customer GetCustomerById(Guid customerId);
        bool CreateTicket(CustomerTicket ticket, CustomerPayment payment, Customer customer);
        Customer GetCustomerByEmail(string username);
    }
}
