using Lumo.Api.Data;
using Lumo.Api.DTOs;
using Lumo.Api.Interfaces;
using Lumo.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lumo.Api.Services
{
    public class CustomerService : ICustomerService
    {
        //        private readonly List<Customer> _customers = new List<Customer>
        //{
        //            new Customer { Id = 1, Name = "Ali", City = "Antalya", CreatedAt = new DateTime(2024, 11, 2) },
        //            new Customer { Id = 2, Name = "Veli", City = "Istanbul", CreatedAt = new DateTime(2024, 12, 15) },
        //            new Customer { Id = 3, Name = "Ayse", City = "Antalya", CreatedAt = new DateTime(2025, 1, 10) },
        //            new Customer { Id = 4, Name = "Fatma", City = "Izmir", CreatedAt = new DateTime(2025, 2, 20) },
        //            new Customer { Id = 5, Name = "Can", City = "Istanbul", CreatedAt = new DateTime(2025, 3, 1) }
        //        };

        private readonly LumoDbContext _dbContext;

        public CustomerService(LumoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer AddCustomer(CreateCustomerRequest customerRequest)
        {
            var customer = new Customer
            {
                Name = customerRequest.Name,
                City = customerRequest.City,
                CreatedAt = DateTime.UtcNow
            };
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            return customer;
        }

        public List<Customer> GetAll(int page = 1, int pageSize = 20, string? city = null)
        {
            IEnumerable<Customer> query = _dbContext.Customers;

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(c => c.City == city);
            }
            
            var pagedCustomers = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return pagedCustomers;
        }

        public Customer? GetById(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
               return null;
            }
            return customer;
        }
    }
}
