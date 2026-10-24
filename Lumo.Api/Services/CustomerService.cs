using Lumo.Api.DTOs;
using Lumo.Api.Interfaces;
using Lumo.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lumo.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly List<Customer> _customers = new List<Customer>
{
            new Customer { Id = 1, Name = "Ali", City = "Antalya", CreatedAt = new DateTime(2024, 11, 2) },
            new Customer { Id = 2, Name = "Veli", City = "Istanbul", CreatedAt = new DateTime(2024, 12, 15) },
            new Customer { Id = 3, Name = "Ayse", City = "Antalya", CreatedAt = new DateTime(2025, 1, 10) },
            new Customer { Id = 4, Name = "Fatma", City = "Izmir", CreatedAt = new DateTime(2025, 2, 20) },
            new Customer { Id = 5, Name = "Can", City = "Istanbul", CreatedAt = new DateTime(2025, 3, 1) },
            new Customer { Id = 6, Name = "Abc", City = "boom", CreatedAt = new DateTime(2025, 4, 5) }
        };

        public Customer AddCustomer(CreateCustomerRequest customerRequest)
        {
            var customer = new Customer
            {
                Name = customerRequest.Name,
                City = customerRequest.City,
                Id = _customers.Max(c => c.Id) + 1,
                CreatedAt = DateTime.UtcNow
            };
            _customers.Add(customer);
            return customer;
        }

        public List<Customer> GetAll(int page = 1, int pageSize = 20, string? city = null)
        {
            IEnumerable<Customer> query = _customers;

            if (city == "boom")
            {
                throw new Exception("Boom city is not allowed.");
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(c => c.City == city);
            }
            
            var pagedCustomers = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return pagedCustomers;
        }

        public Customer? GetById(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
               return null;
            }
            return customer;
        }
    }
}
