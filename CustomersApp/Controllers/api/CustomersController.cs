using CustomersApp.Interfaces;
using System.Web.Http;

namespace CustomersApp.Controllers.api
{
    public class CustomersController : ApiController
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {

            var customer = _customerRepository.GetOneCustomerById(id);

            if (customer == null)
                return NotFound();

            _customerRepository.RemoveCustomer(customer);

            return Ok();
        }
    }
}
