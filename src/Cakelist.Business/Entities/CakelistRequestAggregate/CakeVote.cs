using System;

namespace Cakelist.Business.Entities.CakelistRequestAggregate
{
    public class CakeVote
    {
        public User CreatedBy { get; set;  }
        public DateTimeOffset CreatedAt { get; set; }

        public CakeVote(User createdBy)
        {
            CreatedAt = DateTimeOffset.UtcNow;
            CreatedBy = createdBy;
        }

    }
}