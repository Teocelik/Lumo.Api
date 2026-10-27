using Lumo.Api.DTOs;
using Lumo.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lumo.Api.Interfaces
{
    public interface ICustomerService
    {
        public Task<Customer?> GetByIdAsync(int id);
        public Task<List<Customer>> GetAllAsync(int page = 1, int pageSize = 20, string? city = null);
        public Task<Customer> AddCustomerAsync(CreateCustomerRequest customerRequest);
    }
}
