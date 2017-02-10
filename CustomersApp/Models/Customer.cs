using CustomersApp.Configurations;
using FluentValidation.Attributes;
using System;

namespace CustomersApp.Models
{
    [Validator(typeof(CustomerValidator))]
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreate { get; set; }

        public int Payment { get; set; }

    }
}