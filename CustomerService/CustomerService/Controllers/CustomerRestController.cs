using CustomerService.Service;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerRestController : ControllerBase
    {
        private readonly GetCustomerService _customerService;

        public CustomerRestController(GetCustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_customerService.GetAllCustomers());
        }
    }
}
