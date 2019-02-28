using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cakelist.Api.ApiModels
{
    /// <summary>
    /// API view model to create a vote on a Cake request.
    /// </summary>
    public class CreateVoteModel
    {
        [Required]
        public int CakeRequestId { get; set; }
        [Required]
        public int CreatedById { get; set; }
    }
}
