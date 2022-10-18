using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using LibraryApi.Models;

namespace LibraryApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private CustomerService _customerService = new CustomerService();

        [HttpPost()]
        public IActionResult AddCustomer([FromBody] Customer newCustomer)
        {
            _customerService.AddCustomer(newCustomer);
            return Ok(newCustomer);
        }

    }
}
