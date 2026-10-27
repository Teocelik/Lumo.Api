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
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 20, string? city = null)
        {
            var pagedCustomers = await _customerService.GetAllAsync(page, pageSize, city);
            return Ok(pagedCustomers);
        }


        [HttpPost]
        public async Task<IActionResult> AddCustomer(CreateCustomerRequest customerRequest)
        {
            var customer = await _customerService.AddCustomerAsync(customerRequest);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }
    }
}