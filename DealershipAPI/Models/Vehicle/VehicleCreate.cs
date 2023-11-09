using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace api.Models.Vehicle
{
    public class VehicleCreate
    {
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int ModelYear { get; set; }
        [Required]
        [ForeignKey(nameof(Dealership))]
        public int DealershipId { get; set; }
        public int Stock { get; set; }
    }
}
