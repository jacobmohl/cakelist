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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Cakelist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CakeRequestsController : ControllerBase
    {

        private readonly ILogger _log;
        private readonly ICakelistService _cakelistService;
        private readonly ICakeRequestRepository _cakeRequestRepository;
        private readonly IUserRepository _userRepository;

        public CakeRequestsController(ILogger<CakeRequestsController> logger, ICakelistService cakelistService, ICakeRequestRepository cakeRequestRepository, IUserRepository userRepository)
        {
            _log = logger;
            _cakelistService = cakelistService;
            _cakeRequestRepository = cakeRequestRepository;
            _userRepository = userRepository;
        }


        /// <summary>
        /// Retrieve all cakerequests, also known as the 'Cakelist'.
        /// </summary>
        /// <returns>List of Cake request objects.</returns>
        /// <response code="200">Returns the cakelist</response>
        /// <response code="500">Oops! Something unexpected happened serverside</response>
        [HttpGet(Name = "GetAllCakeRequests")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CakeRequest>))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _cakelistService.GetCakelist());
        }


        /// <summary>
        /// Get a specific cake request by its id.
        /// </summary>
        /// <param name="id">Cake request id.</param>
        /// <returns>Cake request object.</returns>
        /// <response code="200">Cake request</response>
        /// <response code="404">No cake request found with the specified id</response>
        /// <response code="500">Oops! Something unexpected happened serverside</response>
        [HttpGet("{id}", Name = "GetCakeRequestById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CakeRequest))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute, Required] int id)
        {
            // Fetch the request
            var request = await _cakeRequestRepository.GetByIdAsync(id);

            // If non found return 404 - Not found
            if (request == null) {
                return NotFound();
            }

            // Return 200 if found
            return Ok(request);
        }


        /// <summary>
        /// Create cake request.
        /// </summary>
        /// <param name="cakeRequest">Object to contain id of the creator and assignee together with a reason for the cake request.</param>
        /// <returns>The created cake request object.</returns>
        /// <response code="201">Cake request created</response>
        /// <response code="400">The input is not valid or the user id is incorrect</response>
        /// <response code="500">Oops! Something unexpected happened serverside.</response>
        [HttpPost(Name = "CreateCakeRequest")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CakeRequest))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCakeRequestModel cakeRequest)
        {

            if (cakeRequest == null) throw new ArgumentNullException(nameof(cakeRequest));

            // Find users
            //TODO: Throw if we not find the user
            var createdBy = await _userRepository.GetByIdAsync(cakeRequest.CreatedByUserId);
            var assignedTo = await _userRepository.GetByIdAsync(cakeRequest.AssignedToUserId);

            if(createdBy == null || assignedTo == null) {

                return BadRequest(new ProblemDetails {
                    Type = "https://httpstatuses.com/400",
                    Status = 400,
                    Title = "Can't find user",
                    Detail = "CreatedBy or AssignedTo user id is not matching any users."
                });                
            }

            // Create request
            var createdCakeRequest = await _cakelistService.AddCakeRequestAsync(createdBy, assignedTo, cakeRequest.Reason);

            // Return with a 201 and the cake request
            return CreatedAtAction(nameof(GetById), new { Id = createdCakeRequest.Id }, createdCakeRequest);
        }


        //TODO: Create PUT/PATCH endpoint

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, [FromBody] CakeRequest cakeRequest)
        //{
        //    await Task.Run(() => {
        //        return Ok();
        //    });

        //    return BadRequest();
        //}

        //TODO: Create complete endpoint (Cake have been given)

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await Task.Run(() => {
        //        return Ok();
        //    });
        //    return BadRequest();
        //}
    }
}
