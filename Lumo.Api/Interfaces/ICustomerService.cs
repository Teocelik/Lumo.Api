using Lumo.Api.DTOs;
using Lumo.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lumo.Api.Interfaces
{
    public interface ICustomerService
    {
        public Customer? GetById(int id);
        public List<Customer> GetAll(int page = 1, int pageSize = 20, string? city = null);
        public Customer AddCustomer(CreateCustomerRequest customerRequest);
    }
}
