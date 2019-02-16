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

        private readonly ILogger _logger;
        private readonly CakelistContext _cakelistContext;

        public UserRepository(ILogger<UserRepository> logger, CakelistContext cakelistContext)
        {
            _logger = logger;
            _cakelistContext = cakelistContext;
        }

        public async Task<User> AddAsync(User entity)
        {
            var user = _cakelistContext.Users.Add(entity);
            await _cakelistContext.SaveChangesAsync();
            return user.Entity;
        }


        public async Task<User> GetByIdAsync(int id)
        {
            //TODO: Include references
            var user = await _cakelistContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IReadOnlyList<User>> ListAllAsync()
        {
            //TODO: Filter and include references
            var users = await _cakelistContext.Users
                .ToListAsync();
            return users;
        }

        public async Task UpdateAsync(User entity)
        {
            var user = await _cakelistContext.Users.SingleOrDefaultAsync(u => u.Id == entity.Id);

            //TODO: Refactor to update only selected fields
            user = entity;

            _cakelistContext.Users.Update(user);
            await _cakelistContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _cakelistContext.Requests.SingleOrDefaultAsync(u => u.Id == id);
            _cakelistContext.Requests.Remove(user);
            await _cakelistContext.SaveChangesAsync();
        }
    }
}
