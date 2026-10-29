using Lumo.Api.DTOs;
using Lumo.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lumo.Api.Interfaces
{
    public interface ICustomerService
    {
        public Task<CustomerResponse?> GetByIdAsync(int id);
        public Task<List<CustomerResponse>> GetAllAsync(int page = 1, int pageSize = 20, string? city = null);
        public Task<CustomerResponse> AddCustomerAsync(CreateCustomerRequest customerRequest);
    }
}
