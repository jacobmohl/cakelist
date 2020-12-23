using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Cakelist.Api.ApiModels;
using Cakelist.Business.Entities.CakelistRequestAggregate;
using Cakelist.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;

namespace Cakelist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly ICakelistService _cakelistService;
        private readonly IUserRepository _userRepository;

        public VotesController(ICakelistService cakelistService, IUserRepository userRepository)
        {
            _cakelistService = cakelistService;
            _userRepository = userRepository;
        }


        //// GET
        ///// <summary>
        ///// Retrieve all votes from cake request specified by its id.
        ///// </summary>
        ///// <param name="cakeRequestId">Cake request id.</param>
        ///// <returns>List of votes from the Cake request.</returns>
        //[HttpGet(Name = "GetAllVotesByCakeRequestId")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(500)]
        //public async Task<ActionResult<IEnumerable<CakeVote>>> GetAllById([FromQuery, Required] int cakeRequestId)
        //{
        //    //TODO: Implement  
        //    await Task.Run(() => {
        //        throw new NotImplementedException();
        //        //return Ok(new List<CakeVote>());
        //    });

        //    return BadRequest();
        //}

        /// <summary>
        /// Create a vote on a cake request, specified by the user and cake request id.
        /// </summary>
        /// <param name="createVoteModel">Object to contain user and cake request id.</param>
        /// <returns>Created vote object</returns>
        /// <response code="201">Vote created</response>
        /// <response code="400">Input is not valid or user is not found</response>
        [HttpPost(Name = "CreateVote")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CakeVote))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody, BindRequired] CreateVoteModel createVoteModel)
        {

            if(!ModelState.IsValid || createVoteModel == null) {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetByIdAsync(createVoteModel.CreatedById);

            if(user == null) {
                return BadRequest();
            }

            var vote = await _cakelistService.VoteOnCakeRequestAsync(createVoteModel.CakeRequestId, user);

            return CreatedAtAction("GetAllById", new { Id = vote.Id }, vote);

                        
        }
    }
}
