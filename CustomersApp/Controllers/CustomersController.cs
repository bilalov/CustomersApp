using CustomersApp.Interfaces;
using CustomersApp.Models;
using System;
using System.Web.Mvc;

namespace CustomersApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ActionResult Index()
        {
            return View("Index",_customerRepository.GetCustomers());
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", customer);
            }

            customer.DateCreate = DateTime.Now;
            _customerRepository.AddCustomer(customer);

            return RedirectToAction("Index", "Customers");
        }

        [HttpPost]
        public ActionResult Update(Customer _customer)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", _customer);
            }

            var customer = _customerRepository.GetOneCustomerById(_customer.Id);

            if (customer == null)
                return HttpNotFound();

            _customerRepository.UpdateCustomer(_customer);

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _customerRepository.GetOneCustomerById(id);

            if (customer == null)
                return HttpNotFound();

            return View("Edit", customer);
        }
    }
}