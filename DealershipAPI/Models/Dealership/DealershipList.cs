using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Models.Dealership
{
    public class DealershipList
    {

        [Required]
        public int DealershipId { get; set; }
    }
}
