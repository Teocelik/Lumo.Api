using System.ComponentModel.DataAnnotations.Schema;

namespace Lumo.Api.DTOs
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
