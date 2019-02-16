using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakelist.Business.Entities.CakelistRequestAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Cakelist.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VotesController : Controller
    {

        // GET
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CakeVote>>> GetAll([FromQuery] int cakeRequestId)
        {

            await Task.Run(() => {
                return Ok(new List<CakeVote>());
            });

            return BadRequest();
        }

        // GET
        [HttpPost("{cakeRequestId}")]
        public async Task<IActionResult> Post(int cakeRequestId, [FromBody] CakeVote cakeVote)
        {

            await Task.Run(() => {
                return Ok();
            });

            return BadRequest();

        }
    }
}
