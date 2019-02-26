using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cakelist.Api.ViewModels
{
    /// <summary>
    /// API model to create a cake request.
    /// </summary>
    public class CreateCakeRequestModel
    {
        [Required]
        public int CreatedByUserId { get; set; }
        [Required]
        public int AssignedToUserId { get; set; }
        [Required]
        public string Reason { get; set; }
    }
}
