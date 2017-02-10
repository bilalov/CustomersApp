using CustomersApp.Models;
using System;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace CustomersApp.Factories
{
    public class CustomerFactory
    {
        public static Customer MakeCustomer(XElement node)
        {
            return new Customer()
            {
                Id = Int32.Parse(node.Element("Id").Value),
                Name = node.Element("Name").Value,
                DateCreate = Convert.ToDateTime(node.Element("DateCreate").Value),
                Payment = Int32.Parse(node.Element("Payment").Value)
            };
            
        }

        public static Customer MakeCustomer(SqlDataReader reader)
        {
            return  new Customer()
            {
                Name = reader["Name"].ToString(),
                DateCreate = Convert.ToDateTime(reader["DateCreate"].ToString()),
                Payment = Int32.Parse(reader["Payment"].ToString()),
                Id = Int32.Parse(reader["Id"].ToString())
            };
           
        }

        public static XElement MakeXCustomer(Customer customer)
        {
            return new XElement("Customer",
                                new XElement("Id",customer.Id),
                                new XElement("Name", customer.Name),
                                new XElement("DateCreate", customer.DateCreate),
                                new XElement("Payment", customer.Payment)
                                );
        }
    }
}