using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakelist.Api.ViewModels;
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
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return Ok(await _userRepository.ListAllAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
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
        public async Task<IActionResult> Post([FromBody] CreateUserModel user)
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

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] User user)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
