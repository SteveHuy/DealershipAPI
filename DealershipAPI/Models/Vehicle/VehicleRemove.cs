using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.Vehicle
{
    public class VehicleRemove
    {
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
