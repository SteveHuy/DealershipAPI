using System.ComponentModel.DataAnnotations;

namespace api.Models.Dealership
{
    public class DealershipCreate
    {

        [Required]
        public required string Name { get; set; }
    }
}
