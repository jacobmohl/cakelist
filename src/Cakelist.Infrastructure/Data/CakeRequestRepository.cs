using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cakelist.Business.Entities.CakelistRequestAggregate;
using Cakelist.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cakelist.Infrastructure.Data
{
    public class CakeRequestRepository : ICakeRequestRepository
    {

        private readonly ILogger _logger;
        private readonly CakelistContext _cakelistContext;

        public CakeRequestRepository(ILogger<CakeRequestRepository> logger, CakelistContext cakelistContext)
        {
            _logger = logger;
            _cakelistContext = cakelistContext;
        }

        public async Task<CakeRequest> AddAsync(CakeRequest entity)
        {
            _cakelistContext.Requests.Add(entity);
            await _cakelistContext.SaveChangesAsync();
            return entity;            
        }


        public async Task<CakeRequest> GetByIdAsync(int id)
        {
            //TODO: Include references
            var request = await _cakelistContext.Requests
                .Include(r => r.CreatedBy)
                .Include(r => r.AssignedTo)
                .Include(r => r.Votes)
                    .ThenInclude(v => v.CreatedBy)
                .AsNoTracking()
                .SingleOrDefaultAsync(r => r.Id == id);

            return request;
        }

        public async Task<IReadOnlyList<CakeRequest>> ListAllAsync()
        {
            //TODO: Filter and include references
            var requests = await _cakelistContext.Requests
                .Include(r => r.CreatedBy)
                .Include(r => r.AssignedTo)
                .Include(r => r.Votes)
                    .ThenInclude(v => v.CreatedBy)
                .AsNoTracking()
                .ToListAsync();
            return requests;
        }

        public async Task UpdateAsync(CakeRequest entity)
        {
            //var request = await _cakelistContext.Requests.SingleOrDefaultAsync(r => r.Id == entity.Id);

            //TODO: Refactor to update only selected fields
            //request = entity;

            _cakelistContext.Requests.Update(entity);
            await _cakelistContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var request = await _cakelistContext.Requests.SingleOrDefaultAsync(r => r.Id == id);
            _cakelistContext.Requests.Remove(request);
            await _cakelistContext.SaveChangesAsync();
        }
    }
}
