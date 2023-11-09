using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Models.Dealership
{
    public class DealershipSearch
    {
        [Required]
        public int DealershipId { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
    }
}
