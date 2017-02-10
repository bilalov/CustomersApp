using CustomersApp.Models;
using System.Collections.Generic;

namespace CustomersApp.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        IEnumerable<Customer> GetCustomers();
        Customer GetOneCustomerById(int id);
        void RemoveCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
    }
}
