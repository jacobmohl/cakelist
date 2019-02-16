using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cakelist.Business.Entities;
using Cakelist.Business.Entities.CakelistRequestAggregate;

namespace Cakelist.Business.Interfaces
{
    public interface ICakelistService
    {
        Task<IEnumerable<CakeRequest>> GetCakelist();
        Task<CakeRequest> AddCakeRequestAsync(User createdBy, User assignedTo, string reason);
        Task<CakeVote> VoteOnCakeRequestAsync(int cakeRequestId, User voter);
    }
}
