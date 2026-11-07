using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumo.Api.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string City { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
