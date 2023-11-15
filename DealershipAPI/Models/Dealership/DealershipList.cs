using System.ComponentModel.DataAnnotations;

namespace api.Models.Dealership
{
    public class DealershipList
    {

        [Required]
        public int DealershipId { get; set; }
    }
}
