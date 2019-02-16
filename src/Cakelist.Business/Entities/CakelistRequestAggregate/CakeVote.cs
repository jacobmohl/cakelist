using System;

namespace Cakelist.Business.Entities.CakelistRequestAggregate
{
    public class CakeVote
    {
        public int Id { get; set; }
        public User CreatedBy { get; set;  }
        public DateTimeOffset CreatedAt { get; set; }

        public int CakeRequestId { get; set; }
        //public CakeRequest CakeRequest { get; set; }

        public CakeVote() { }

        public CakeVote(User createdBy)
        {
            CreatedAt = DateTimeOffset.UtcNow;
            CreatedBy = createdBy;
        }

    }
}