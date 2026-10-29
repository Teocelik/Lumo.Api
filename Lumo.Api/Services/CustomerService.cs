using Lumo.Api.Data;
using Lumo.Api.DTOs;
using Lumo.Api.Interfaces;
using Lumo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lumo.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly LumoDbContext _dbContext;

        public CustomerService(LumoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private CustomerResponse MapToCustomerResponse(Customer customer)
        {
            return new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                City = customer.City,
                CreatedAt = customer.CreatedAt
            };
        }


        public async Task<CustomerResponse> AddCustomerAsync(CreateCustomerRequest customerRequest)
        {
            var customer = new Customer
            {
                Name = customerRequest.Name,
                City = customerRequest.City,
                CreatedAt = DateTime.UtcNow,
            };

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            return MapToCustomerResponse(customer);
        }

        public async Task<List<CustomerResponse>> GetAllAsync(int page = 1, int pageSize = 20, string? city = null)
        {
            IQueryable<Customer> query = _dbContext.Customers;

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(c => c.City == city);
            }

            var pagedCustomers = await query.OrderBy(c => c.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var responseList = pagedCustomers.Select(c => MapToCustomerResponse(c)).ToList();

            return responseList;
        }

        public async Task<CustomerResponse?> GetByIdAsync(int id)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if(customer == null)
            {
                return null;
            }

            return MapToCustomerResponse(customer);
        }
    }
}
