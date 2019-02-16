using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakelist.Business.Entities.CakelistRequestAggregate;
using Cakelist.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cakelist.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CakeRequestsController : Controller
    {

        private readonly ICakelistService _cakelistService;
        private readonly ICakeRequestRepository _cakeRequestRepository;

        public CakeRequestsController(ICakelistService cakelistService, ICakeRequestRepository cakeRequestRepository)
        {
            _cakelistService = cakelistService;
            _cakeRequestRepository = cakeRequestRepository;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CakeRequest>>> GetAll()
        {
            return Ok(await _cakelistService.GetCakelist());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CakeRequest>> Get(int id)
        {
            return Ok(await _cakeRequestRepository.GetByIdAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CakeRequest cakeRequest)
        {
            await Task.Run(() => {
                return Ok();
            });

            return BadRequest();

        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CakeRequest cakeRequest)
        {
            await Task.Run(() => {
                return Ok();
            });

            return BadRequest();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Run(() => {
                return Ok();
            });
            return BadRequest();

        }
    }
}
