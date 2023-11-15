using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.Dealership
{
    public class VehicleStock
    {

        [Required]
        public int Stock { get; set; }
        [Required]
        public required string Make { get; set; }
        [Required]
        public required string Model { get; set; }
        [Required]
        public int ModelYear { get; set; }
        [Required]
        [ForeignKey(nameof(Dealership))]
        public int DealershipId { get; set; }
    }
}
