using System;
using System.Runtime.Serialization;

namespace Cakelist.Business.Entities.CakelistRequestAggregate
{
    [Serializable]
    public class VoterHasVotedException : Exception
    {
        public VoterHasVotedException()
        {
        }

        public VoterHasVotedException(string message) : base(message)
        {
        }

        public VoterHasVotedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VoterHasVotedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}