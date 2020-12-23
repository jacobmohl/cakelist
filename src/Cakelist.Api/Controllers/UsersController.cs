using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Cakelist.Api.ApiModels;
using Cakelist.Business.Entities;
using Cakelist.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Cakelist.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        // GET api/values
        /// <summary>
        /// Retrieve all active users, with no additional data
        /// </summary>
        /// <returns>List of User objects</returns>
        /// <response code="200">List of users</response>
        [HttpGet(Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        public async Task<IActionResult> GetAll()
        {
            //TODO: Could be extended with possibility to fetch cakerequests and votes.
            return Ok(await _userRepository.ListAllAsync());
        }

        // GET api/values/5
        /// <summary>
        /// Get a specific user by its id.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User object</returns>
        /// <response code="200">User object</response>
        /// <response code="404">Could not find user with specified id</response>
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
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
        /// <summary>
        /// Create a user and persists it.s
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="201">User created</response>
        /// <response code="400">The input is not valid.</response>
        [HttpPost(Name = "CreateUser")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserModel user)
        {

            if (!ModelState.IsValid || user == null) {
                return BadRequest(ModelState);
            }

            var createdUser = await _userRepository.AddAsync(
                new User {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });

            return CreatedAtAction(nameof(GetById), new { Id = createdUser.Id }, createdUser);

        }


        //TODO: Update user with PUT

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] User user)
        //{
        //}

    }
}
