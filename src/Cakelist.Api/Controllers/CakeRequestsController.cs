using System;
using System.Collections.Generic;
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
    public class CakeRequestsController : Controller
    {

        private readonly ICakelistService _cakelistService;
        private readonly ICakeRequestRepository _cakeRequestRepository;
        private readonly IUserRepository _userRepository;

        public CakeRequestsController(ICakelistService cakelistService, ICakeRequestRepository cakeRequestRepository, IUserRepository userRepository)
        {
            _cakelistService = cakelistService;
            _cakeRequestRepository = cakeRequestRepository;
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CakeRequest>>> GetAll()
        {
            return Ok(await _cakelistService.GetCakelist());
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CakeRequest>> GetById(int id)
        {
            // Fetch the request
            var request = await _cakeRequestRepository.GetByIdAsync(id);

            // If non found return 404 - Not found
            if(request == null) {
                return NotFound();
            }

            // Return 200 if found
            return Ok(request);
        }


        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CakeRequest>> Create([FromBody] CreateCakeRequestModel cakeRequest)
        {

            // Chech if the modelstate / model is valid, if not return 400 - Bad request
            if (!ModelState.IsValid) {
                BadRequest(ModelState);
            }

            // Find users
            //TODO: Throw if we not find the user
            var createdBy = await _userRepository.GetByIdAsync(cakeRequest.CreatedByUserId);
            var assignedTo = await _userRepository.GetByIdAsync(cakeRequest.AssignedToUserId);

            // Create request
            var createdCakeRequest = await _cakelistService.AddCakeRequestAsync(createdBy, assignedTo, cakeRequest.Reason);

            // Return with a 201 and the cake request
            return CreatedAtAction(nameof(GetById), new { id = createdCakeRequest.Id }, createdCakeRequest);
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
