using System;
using System.Runtime.Serialization;

namespace Cakelist.Business.Exceptions
{
    [Serializable]
    public class VoterIsAsigneeException : Exception
    {
        public VoterIsAsigneeException()
        {
        }

        public VoterIsAsigneeException(string message) : base(message)
        {
        }

        public VoterIsAsigneeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VoterIsAsigneeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}