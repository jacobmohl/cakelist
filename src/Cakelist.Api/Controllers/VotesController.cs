using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Cakelist.Api.ApiModels;
using Cakelist.Business.Entities.CakelistRequestAggregate;
using Cakelist.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cakelist.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VotesController : Controller
    {
        private readonly ICakelistService _cakelistService;
        private readonly IUserRepository _userRepository;

        public VotesController(ICakelistService cakelistService, IUserRepository userRepository)
        {
            _cakelistService = cakelistService;
            _userRepository = userRepository;
        }


        // GET
        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<CakeVote>>> GetAll([FromQuery, Required] int cakeRequestId)
        {
            //TODO: Implement  
            await Task.Run(() => {
                throw new NotImplementedException();
                return Ok(new List<CakeVote>());
            });

            return BadRequest();
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(CakeVote),201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] CreateVoteModel createVoteModel)
        {

            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetByIdAsync(createVoteModel.CreatedById);

            if(user == null) {
                return BadRequest( new Exception("No user found") );
            }

            try {
                var vote = await _cakelistService.VoteOnCakeRequestAsync(createVoteModel.CakeRequestId, user);

                return Created(nameof(GetAll), vote);
            }
            catch (Exception e) {
                return BadRequest(e);
            }
                        
        }
    }
}
