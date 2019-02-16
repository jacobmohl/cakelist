using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cakelist.Business.Entities;
using Cakelist.Business.Entities.CakelistRequestAggregate;
using Cakelist.Business.Interfaces;

namespace Cakelist.Business.Services
{
    public class CakelistService : ICakelistService
    {
        private readonly ICakeRequestRepository _cakeRequestRepository;
        private readonly IUserNotificationService _userNotificationService;

        public CakelistService(ICakeRequestRepository cakeRequestRepository, IUserNotificationService userNotificationService)
        {
            _cakeRequestRepository = cakeRequestRepository;
            _userNotificationService = userNotificationService;
        }

        public async Task<IEnumerable<CakeRequest>> GetCakelist()
        {
            //TODO: Maybe some filtering of old requests ect.
            return await _cakeRequestRepository.ListAllAsync();
        }


        public async Task<CakeRequest> AddCakeRequestAsync(User createdBy, User assignedTo, string reason)
        {
            var request = new CakeRequest(createdBy, assignedTo, reason);

            await _cakeRequestRepository.AddAsync(request);

            var subject = "You have been assigned a cake request";
            var message =
                $"Hello {assignedTo.FullName()}" +
                $"You have been assigned a cake request by {createdBy.FullName()} on the Cakelist with the reason: {reason}." +
                $"Cake request id: {request.Id}" +
                $"Number of votes: {request.Votes.Count}" +
                $"" +
                $"Best regards" +
                $"Your friendly Cakelist";

            await _userNotificationService.NotifyUserAsync(assignedTo, subject, message);

            return request;
        }


        public async Task<CakeVote> VoteOnCakeRequestAsync(int cakeRequestId, User voter)
        {
            var request = await _cakeRequestRepository.GetByIdAsync(cakeRequestId);
            var vote = request.AddVote(voter);

            await _cakeRequestRepository.UpdateAsync(request);

            var subject = "Your cake request have recieved a vote";
            var message =
                $"Hello {request.AssignedTo.FullName()}" +
                $"You cake request with id: {request.Id} has got a new vote from {voter.FullName()} and has now {request.Votes.Count} votes" +
                $"" +
                $"Best regards" +
                $"Your friendly Cakelist";

            await _userNotificationService.NotifyUserAsync(request.AssignedTo, subject, message);

            return vote;
        }
    }
}
