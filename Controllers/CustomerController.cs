using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using LibraryApi.Models;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Net;

namespace LibraryApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private CustomerService _customerService = new CustomerService();

        [HttpGet()]
        public IActionResult GetAll()
        {
            IEnumerable<Customer> customerList =_customerService.GetAllCustomers();
            return Ok(customerList);
        }

        [HttpGet("{customerId}")]
        public IActionResult GetDetails(int customerId)
        {
            Customer customer = _customerService.GetCustomerById(customerId);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Customer newCustomer)
        {
            _customerService.AddCustomer(newCustomer);
            return Created($"/{newCustomer.Id}", newCustomer);
        }

        [HttpDelete("{customerId}")]
        public IActionResult Delete(int customerId)
        {
            _customerService.DeleteCustomer(customerId);
            return NoContent();
        }

        [HttpPut("{customerId}")]
        public IActionResult Put([FromBody] Customer customer, int customerId)
        {
            var customerToUpdate = _customerService.UpdateCustomer(customerId, customer);
            if (customerToUpdate != null)
                return Created($"/{customerId}", customer);
            return NoContent();
        }

    }
}
