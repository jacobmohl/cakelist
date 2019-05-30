using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cakelist.Business.Entities;
using Cakelist.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cakelist.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {

        private readonly ILogger _log;
        private readonly CakelistContext _cakelistContext;

        public UserRepository(ILogger<UserRepository> logger, CakelistContext cakelistContext)
        {
            _log = logger;
            _cakelistContext = cakelistContext;
        }

        public async Task<User> AddAsync(User entity)
        {
            _log.LogInformation($"Adding user with name: {entity.FullName()}");

            var user = _cakelistContext.Users.Add(entity);
            await _cakelistContext.SaveChangesAsync();

            _log.LogInformation($"User added with id {entity.Id}");

            return user.Entity;
        }


        public async Task<User> GetByIdAsync(int id)
        {
            _log.LogInformation($"Getting user with id: {id}");

            //TODO: Include references
            var user = await _cakelistContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);

            _log.LogInformation("User getted");

            return user;
        }

        public async Task<IReadOnlyList<User>> ListAllAsync()
        {
            _log.LogInformation("Getting all users");
            //TODO: Filter and include references
            var users = await _cakelistContext.Users
                .ToListAsync();

            _log.LogInformation("All users getted");

            return users;
        }

        public async Task UpdateAsync(User entity)
        {
            _log.LogInformation($"Updating user with id {entity.Id}");

            var user = await _cakelistContext.Users.SingleOrDefaultAsync(u => u.Id == entity.Id);

            //TODO: Refactor to update only selected fields
            user = entity;
            _cakelistContext.Users.Update(user);

            _log.LogInformation("User updated");

            await _cakelistContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        { 
      
            _log.LogInformation($"Deleting user with {id}");

            var user = await _cakelistContext.Requests.SingleOrDefaultAsync(u => u.Id == id);
            _cakelistContext.Requests.Remove(user);
            await _cakelistContext.SaveChangesAsync();

            _log.LogInformation("User deleted");
        }
    }
}
