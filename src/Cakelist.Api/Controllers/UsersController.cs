using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakelist.Api.ApiModels;
using Cakelist.Business.Entities;
using Cakelist.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cakelist.Api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await _userRepository.ListAllAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<User>> GetById(int id)
        {

            // Fetch a user
            var user = await _userRepository.GetByIdAsync(id);

            // If non found, then return 404 Not Found
            if(user == null) 
            {
                return NotFound();
            }

            // Else return 200 with the user
            return Ok(user);
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] CreateUserModel user)
        {
            if (!ModelState.IsValid) 
            {
                BadRequest(ModelState);
            }

            var createdUser = await _userRepository.AddAsync(
                new User {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });

            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);

        }


        //TODO: Create PUT and PATH endpoints

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] User user)
        //{
        //}

    }
}
