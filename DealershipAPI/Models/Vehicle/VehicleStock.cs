using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.Dealership
{
    public class VehicleStock
    {

        [Required]
        public int Stock { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int ModelYear { get; set; }
        [Required]
        [ForeignKey(nameof(Dealership))]
        public int DealershipId { get; set; }
    }
}
