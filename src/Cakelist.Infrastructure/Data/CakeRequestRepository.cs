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

        private readonly ILogger _log;
        private readonly CakelistContext _cakelistContext;

        public CakeRequestRepository(ILogger<CakeRequestRepository> logger, CakelistContext cakelistContext)
        {
            _log = logger;
            _cakelistContext = cakelistContext;
        }

        public async Task<CakeRequest> AddAsync(CakeRequest entity)
        {
            _log.LogInformation($"Adding cake request from user id {entity.CreatedById} to user id {entity.AssignedToId}");
            _cakelistContext.Requests.Add(entity);
            await _cakelistContext.SaveChangesAsync();
            _log.LogInformation($"Added cake request with id: {entity.Id}");

            return entity;            
        }


        public async Task<CakeRequest> GetByIdAsync(int id)
        {
            _log.LogInformation($"Getting cake request with id {id}");

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
            _log.LogInformation("Getting all cake requests");

            //TODO: Filter and include references
            var requests = await _cakelistContext.Requests
                .Include(r => r.CreatedBy)
                .Include(r => r.AssignedTo)
                .Include(r => r.Votes)
                    .ThenInclude(v => v.CreatedBy)
                .AsNoTracking()
                .ToListAsync();

            _log.LogInformation("Cake requests getted.");
            return requests;
        }

        public async Task UpdateAsync(CakeRequest entity)
        {
            _log.LogInformation($"Updating cake request with id: {entity.Id}");
            //var request = await _cakelistContext.Requests.SingleOrDefaultAsync(r => r.Id == entity.Id);

            //TODO: Refactor to update only selected fields
            //request = entity;

            _cakelistContext.Requests.Update(entity);
            await _cakelistContext.SaveChangesAsync();

            _log.LogInformation("Cake request updated");
        }
        public async Task DeleteAsync(int id)
        {
            _log.LogInformation($"Deleting cake request with id: {id}");

            var request = await _cakelistContext.Requests.SingleOrDefaultAsync(r => r.Id == id);
            _cakelistContext.Requests.Remove(request);
            await _cakelistContext.SaveChangesAsync();

            _log.LogInformation("Cake request deleted");
        }
    }
}
