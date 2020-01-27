using System;
using System.Runtime.Serialization;

namespace BowlingGame.Models
{
    [Serializable]
    public class InvalidRollException : ArgumentOutOfRangeException
    {
        public InvalidRollException()
        {
        }

        public InvalidRollException(string message) : base(message)
        {
        }

        public InvalidRollException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidRollException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}