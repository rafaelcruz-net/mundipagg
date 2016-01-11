using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MundiPagg.Web.ModelView
{
    public class CreateCustomerModelView
    {
        public String Name { get; set; }
        public String CPF { get; set; }
        public DateTime Birthday { get; set; }
        public String Genre { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String ConfirmPassword { get; set; }
        public CreateCustomerAddressModelView Address { get; set; }
    }

    public class CreateCustomerAddressModelView
    {
        public String Cep { get; set; }
        public String Address { get; set; }
        public String Complement { get; set; }
        public String Number { get; set; }
        public String Neighbor { get; set; }
        public String City { get; set; }
        public String State { get; set; }
    }
}