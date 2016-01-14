using MundiPagg.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MundiPagg.Web.ModelView
{
    public class CreateCustomerModelView : BaseModelView<Customer>
    {
        public String Name { get; set; }
        public String CPF { get; set; }
        public DateTime Birthday { get; set; }
        public String Genre { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String ConfirmPassword { get; set; }
        public CreateCustomerAddressModelView Address { get; set; }

        public override Customer ToDomain()
        {
            Customer customer = new Customer()
            {
                Birthday = this.Birthday,
                CPF = this.CPF,
                Email = this.Email,
                Genre = this.Genre,
                Name = this.Name,
                Password = this.Password
            };

            if (this.Address != null)
            {
                customer.Address.Add(new CustomerAddress()
                {
                    Address = this.Address.Address,
                    Cep = this.Address.Cep,
                    CityId = Convert.ToInt32(this.Address.City),
                    Complement = this.Address.Complement,
                    Neighbor = this.Address.Neighbor,
                    Number = this.Address.Number,
                });
            }

            return customer;
        }
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