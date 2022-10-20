using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Net;
using LibraryApi.Models.Customers;
using LibraryApi.Services.Interfaces;

namespace LibraryApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private ICustomerService _customerService;
        private IBorrowService _borrowService;

        public CustomerController (ICustomerService customerService, IBorrowService borrowService)
        {
            _customerService = customerService;
            _borrowService = borrowService;
        }

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

        [HttpGet("{customerId}/borrow")]
        public IActionResult GetBorrowsByCustomer(int customerId)
        {
            var borrow = _borrowService.GetAllBorrowByCustomerId(customerId);
            if (borrow == null)
                return NotFound();
            return Ok(borrow);
        }

        [HttpGet("mostborrow/customers")]
        public IActionResult GetCustomersWithMostBorrows()
        {
            List<Customer> customers = _customerService.GetMostCustomerWithBorrow();
            if (customers == null)
                return NotFound();
            return Ok(customers);
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
