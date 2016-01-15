using log4net;
using MundiPagg.Domain.Service.Interfaces;
using MundiPagg.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using WebMatrix.WebData;

namespace MundiPagg.Domain.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ICustomerTicketService customerTicketService;
        ILog Log = log4net.LogManager.GetLogger("MundiPagg.Log");

        public CustomerService(ICustomerRepository _repository, ICustomerTicketService _customerTicketService)
        {
            this.customerRepository = _repository;
            this.customerTicketService = _customerTicketService;
        }

        public void CreateCustomer(Customer customer)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Log.Info($"Criando um novo usuário com o CPF {customer.CPF}");

                    if (this.ExistsCustomer(customer))
                        throw new Exception($"CPF {customer.CPF} já cadastrado");

                    var userId = Guid.NewGuid();
                    customer.Id = userId;
                    customer.Password = Infra.Utils.SecurityUtils.HashSHA1(customer.Password);

                    this.customerRepository.Save(customer);

                    if (!this.Login(customer.Email, customer.Password, true, false))
                        throw new Exception("Usuário não cadastrado, por favor tente mais tarde");

                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, ex);
                    this.LogOut();
                    throw;
                }
                finally
                {
                    Log.Info($"Finalizando a criação de um novo usuário com o CPF {customer.CPF}");
                    scope.Complete();
                }
            }
        }

        public bool Login(string email, string password, bool rememberMe, bool needsEncryptPassword = true)
        {
            try
            {
                Log.Info($"Logando o usuário com o email {email}");

                string hashedPwd = password;

                if (needsEncryptPassword)
                    hashedPwd = Infra.Utils.SecurityUtils.HashSHA1(password);

                var result = WebSecurity.Login(email, hashedPwd, rememberMe);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                Log.Info($"Finalizando o login do usuário com o email {email}");
            }
        }

        public void LogOut()
        {
            try
            {
                Log.Info("Deslogando o usuário corrente");

                //Abandona a sessão
                HttpContext.Current.Session.Abandon();

                //Desliga o usuário do sistema
                WebSecurity.Logout();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                Log.Info("Finalizando o deslogando o usuário corrente");
            }
        }

        public Customer GetCustomerByEmailAndPassword(string email, string password)
        {
            try
            {
                Log.Info($"Consultando o usuário com o email {email}");
                return this.customerRepository.GetCustomerByEmailAndPassword(email,password);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                Log.Info($"Finalizando a consulta o usuário com o id {email}");
            }

        }

        private bool ExistsCustomer(Customer customer)
        {
            try
            {
                Log.Info($"Consultando o usuário com o CPF {customer.CPF}");
                return this.customerRepository.GetCustomerByCPF(customer.CPF) != null;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                Log.Info($"Finalizando a consulta o usuário com o CPF {customer.CPF}");
            }
        }

        public Customer GetCustomerById(Guid customerId)
        {
            try
            {
                Log.Info($"Consultando o usuário com o id {customerId}");

                return this.customerRepository.GetCustomerById(customerId);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                Log.Info($"Finalizando a consulta o usuário com o id {customerId}");
            }


        }

        public bool CreateTicket(CustomerTicket ticket, CustomerPayment payment ,Customer customer)
        {
            try
            {
                Log.Info($"Criando o ticket do cliente com o id {customer.Id}");

                ticket.Status = Enum.StatusEnum.Pending;
                ticket.Id = Guid.NewGuid();

                var result = this.customerTicketService.CreateTicket(ticket, payment, customer);

                if (result)
                {
                    customer.Tickets.Add(ticket);
                    this.customerRepository.Update(customer);
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                Log.Info($"Finalizando a criação do ticket do evento para o cliente {customer.Id}");
            }

        }

        public Customer GetCustomerByEmail(string username)
        {
            try
            {
                Log.Info($"Consultando o usuário com o id {username}");

                return this.customerRepository.GetCustomerByEmail(username);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                Log.Info($"Finalizando a consulta o usuário com o id {username}");
            }

        }

        public void Update(Customer customer)
        {
            try
            {
                Log.Info($"Atualizando o usuário com o id {customer.Id}");
                this.customerRepository.Update(customer);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                Log.Info($"Finalizando a atualização o usuário com o id {customer.Id}");
            }

        }

        public bool CreateQuickTicket(CustomerTicket ticket, CustomerPayment payment, Customer customer)
        {
            try
            {
                Log.Info($"Criando o ticket do cliente com o id {customer.Id}");

                ticket.Status = Enum.StatusEnum.Pending;
                ticket.Id = Guid.NewGuid();

                var result = this.customerTicketService.CreateQuickTicket(ticket, payment, customer);

                if (result)
                {
                    customer.Tickets.Add(ticket);
                    this.customerRepository.Update(customer);
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                Log.Info($"Finalizando a criação do ticket do evento para o cliente {customer.Id}");
            }

        }
    }
}
