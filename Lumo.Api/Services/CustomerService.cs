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

        public async Task<Customer> AddCustomerAsync(CreateCustomerRequest customerRequest)
        {
            var customer = new Customer
            {
                Name = customerRequest.Name,
                City = customerRequest.City,
                CreatedAt = DateTime.UtcNow
            };
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<List<Customer>> GetAllAsync(int page = 1, int pageSize = 20, string? city = null)
        {
            IQueryable<Customer> query = _dbContext.Customers;

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(c => c.City == city);
            }

            var pagedCustomers = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return pagedCustomers;
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
         
            return customer;
        }
    }
}
