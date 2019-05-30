using System;

namespace Cakelist.Business.Entities.CakelistRequestAggregate
{
    public class CakeVote
    {
        public int Id { get; set; }

        public int CreatedById { get; set;  }
        public User CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public int CakeRequestId { get; set; }

        public CakeVote() { }

        public CakeVote(int createdById)
        {
            CreatedAt = DateTimeOffset.UtcNow;
            CreatedById = createdById;
        }

    }
}