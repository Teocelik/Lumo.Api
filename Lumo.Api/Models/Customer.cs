using System.ComponentModel.DataAnnotations.Schema;

namespace Lumo.Api.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
