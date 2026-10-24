using Lumo.Api.DTOs;
using Lumo.Api.Interfaces;
using Lumo.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lumo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 20, string? city = null)
        {
            var pagedCustomers = _customerService.GetAll(page, pageSize, city);
            return Ok(pagedCustomers);
        }


        [HttpPost]
        public IActionResult AddCustomer(CreateCustomerRequest customerRequest)
        {
            var customer = _customerService.AddCustomer(customerRequest);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }
    }
}