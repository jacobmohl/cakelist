using System.ComponentModel.DataAnnotations;

namespace Cakelist.Web.Models.CakeRequest
{
    public class CreateCakeRequest
    {
        [Required]
        public int CreatedByUserId { get; set; }
        [Required]
        public int AssignedToUserId { get; set; }
        [Required]
        public string Reason { get; set; }
    }
}
