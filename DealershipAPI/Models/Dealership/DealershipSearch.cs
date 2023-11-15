using System.ComponentModel.DataAnnotations;

namespace api.Models.Dealership
{
    public class DealershipSearch
    {
        [Required]
        public int DealershipId { get; set; }
        [Required]
        public required string Make { get; set; }
        [Required]
        public required string Model { get; set; }
    }
}
