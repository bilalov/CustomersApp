using CustomersApp.Factories;
using CustomersApp.Helpers;
using CustomersApp.Interfaces;
using CustomersApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CustomersApp.Repositories
{
    public class CustomerRepositoryXml : ICustomerRepository
    {
        private string connectionString
        {
            get
            {
                return HttpContext.Current.Server.MapPath(ConfigurationManager.ConnectionStrings["Xmlconnection"].ConnectionString);
            }
        }


        private int IncreaseCounterIdentificators(XElement document)
        {
            int counter = Int32.Parse(document.Attribute("maxId").Value) + 1;
            document.Attribute("maxId").Value = counter.ToString();

            return counter;
        }

        public void AddCustomer(Customer _customer)
        {
            try
            {
                XElement doc = XElement.Load(connectionString);

                _customer.Id = IncreaseCounterIdentificators(doc);
                XElement customer = CustomerFactory.MakeXCustomer(_customer);

                doc.Add(customer);
                doc.Save(connectionString);
            }
            catch (Exception)
            {
                throw new Exception(ErrorTypes.ERROR_INSERT_CLIENT);
            }
        }

        public IEnumerable<Customer> GetCustomers()
        {
            try
            {
                XElement doc = XElement.Load(connectionString);

                IEnumerable<Customer> customers = (from node in doc.Elements("Customer")
                                                   select CustomerFactory.MakeCustomer(node));

                return customers;
            }
            catch (Exception)
            {
                throw new Exception(ErrorTypes.ERROR_GETLIST_CLIENT);
            }
        }

        public Customer GetOneCustomerById(int id)
        {
            try
            {
                XElement doc = XElement.Load(connectionString);

                Customer customer = (from node in doc.Elements("Customer")
                                     where node.Element("Id").Value == id.ToString()
                                     select CustomerFactory.MakeCustomer(node))
                                     .SingleOrDefault();

                return customer;
            }
            catch (Exception)
            {
                throw new Exception(ErrorTypes.ERROR_GETONE_CLIENT);
            }
        }

        public void RemoveCustomer(Customer _customer)
        {
            try
            {
                XElement doc = XElement.Load(connectionString);

                XElement customer = (from node in doc.Elements("Customer")
                                     where node.Element("Id").Value == _customer.Id.ToString()
                                     select node).SingleOrDefault();

                if (customer != null)
                {
                    customer.Remove();
                }

                doc.Save(connectionString);
            }
            catch (Exception)
            {
                throw new Exception(ErrorTypes.ERROR_DELETE_CLIENT);
            }
        }

        public void UpdateCustomer(Customer _customer)
        {
            try
            {
                XElement doc = XElement.Load(connectionString);

                XElement customer = (from node in doc.Elements("Customer")
                                     where node.Element("Id").Value == _customer.Id.ToString()
                                     select node).SingleOrDefault();

                if (customer != null)
                {
                    customer.ReplaceAll(CustomerFactory.MakeXCustomer(_customer).Elements());
                }

                doc.Save(connectionString);
            }
            catch (Exception)
            {
                throw new Exception(ErrorTypes.ERROR_UPDATE_CLIENT);
            }
        }
    }
}