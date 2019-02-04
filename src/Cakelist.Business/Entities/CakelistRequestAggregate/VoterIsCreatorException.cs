using System;
using System.Runtime.Serialization;

namespace Cakelist.Business.Entities.CakelistRequestAggregate
{
    [Serializable]
    public class VoterIsCreatorException : Exception
    {
        public VoterIsCreatorException()
        {
        }

        public VoterIsCreatorException(string message) : base(message)
        {
        }

        public VoterIsCreatorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VoterIsCreatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}