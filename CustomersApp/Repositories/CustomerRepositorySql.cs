using CustomersApp.Factories;
using CustomersApp.Helpers;
using CustomersApp.Interfaces;
using CustomersApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CustomersApp.Repositories
{
    public class CustomerRepositorySql:ICustomerRepository
    {
        private string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

        private SqlConnection GetSqlConnection()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        
        public void AddCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection conn = GetSqlConnection())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO Customers(Name, DateCreate, Payment) VALUES(@Name, @DateCreate, @Payment)";
                        cmd.Parameters.AddWithValue("@Name", customer.Name);
                        cmd.Parameters.AddWithValue("@DateCreate", customer.DateCreate);
                        cmd.Parameters.AddWithValue("@Payment", customer.Payment);
                        cmd.ExecuteNonQuery();

                    }
                 }
            }
            catch (Exception ex)
            {
                throw new Exception(ErrorTypes.ERROR_INSERT_CLIENT);
            }
           
        }

        public IEnumerable<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                using (SqlConnection conn = GetSqlConnection())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT * FROM Customers";

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Customer customer = CustomerFactory.MakeCustomer(reader);
                            customers.Add(customer);
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw new Exception(ErrorTypes.ERROR_GETLIST_CLIENT);
            }
            

            return customers;
        }

        public Customer GetOneCustomerById(int id)
        {
            Customer customer = new Customer();

            try
            {
                using (SqlConnection conn = GetSqlConnection())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT * FROM Customers WHERE id = @Id";
                        cmd.Parameters.AddWithValue("@Id", id);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            customer = CustomerFactory.MakeCustomer(reader);
                        }

                    }
                }
            }
            catch (Exception)
            {
                
                throw new Exception(ErrorTypes.ERROR_GETONE_CLIENT);
            }

            return customer;
        }

        public void RemoveCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection conn = GetSqlConnection())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM Customers WHERE Id = @Id";
                        cmd.Parameters.AddWithValue("@Id", customer.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception(ErrorTypes.ERROR_DELETE_CLIENT);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection conn = GetSqlConnection())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE Customers SET Name = @Name, Payment = @Payment WHERE Id = @Id";
                        cmd.Parameters.AddWithValue("@Name", customer.Name);
                        cmd.Parameters.AddWithValue("@Id", customer.Id);
                        cmd.Parameters.AddWithValue("@Payment", customer.Payment);
                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception)
            {

                throw new Exception(ErrorTypes.ERROR_UPDATE_CLIENT);
            }
        }
    }
}