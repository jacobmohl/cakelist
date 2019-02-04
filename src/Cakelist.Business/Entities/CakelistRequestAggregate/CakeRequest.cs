using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cakelist.Business.Entities.CakelistRequestAggregate
{
    public class CakeRequest
    {
        public Guid Id { get; }
        public User CreatedBy { get; }
        public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;

        public User AssignedTo { get; }
        public string Reason { get; }
        public CakeRequestStatus Status { get; set; } = CakeRequestStatus.NotConfirmed;

        public List<CakeVote> Votes = new List<CakeVote>();

        public CakeRequest(User createdBy, User assignedTo, string reason)
        {
            Id = new Guid();
            CreatedBy = createdBy;
            AssignedTo = assignedTo;
            Reason = reason;
        }

        public CakeVote AddVote(User voter)
        {
            // Implementation of busniess logic requirment #L1, see README.md
            if (Votes.Any(v => v.CreatedBy == voter)) { throw new VoterHasVotedException("User has already voted on this cake request."); };

            // Implementation of busniess logic requirment #L2, see README.md
            if (voter == CreatedBy) { throw new VoterIsCreatorException("User cant vote in his/her own cake request."); };

            // Implementation of busniess logic requirment #L3, see README.md
            if (voter == AssignedTo) { throw new VoterIsAsigneeException("User cant vote on cake request there he/her is assignee."); };

            // If the business logic is respected, then create the vote and add it to the list.
            var vote = new CakeVote(voter);
            Votes.Add(vote);

            return vote;
        }
    }
}
