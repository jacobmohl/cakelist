using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cakelist.Business.Exceptions;

namespace Cakelist.Business.Entities.CakelistRequestAggregate
{
    public class CakeRequest
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public int AssignedToId { get; set; }
        public User AssignedTo { get; set; }

        public string Reason { get; set; }
        public CakeRequestStatus Status { get; set; }

        public List<CakeVote> Votes { get; set; } = new List<CakeVote>();

        public CakeRequest() { }

        public CakeRequest(User createdBy, User assignedTo, string reason)
        {
            CreatedAt = DateTimeOffset.UtcNow;
            CreatedById = createdBy.Id;
            AssignedToId = assignedTo.Id;
            Reason = reason;
            Votes = new List<CakeVote>();
            Status = CakeRequestStatus.NotConfirmed;
        }

        public CakeVote AddVote(User voter)
        {
            // Implementation of busniess logic requirment #L1, see README.md
            if (Votes != null && Votes.Any(v => v.CreatedById == voter.Id)) { throw new VoterHasVotedException("User has already voted on this cake request."); };

            // Implementation of busniess logic requirment #L2, see README.md
            if (voter.Id == CreatedById) { throw new VoterIsCreatorException("User cant vote in his/her own cake request."); };

            // Implementation of busniess logic requirment #L3, see README.md
            if (voter.Id == AssignedToId) { throw new VoterIsAsigneeException("User cant vote on cake request there he/her is assignee."); };

            // If the business logic is respected, then create the vote and add it to the list.
            var vote = new CakeVote(voter.Id);
            Votes.Add(vote);

            return vote;
        }
    }
}
