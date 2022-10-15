using System.Runtime.Serialization;

namespace ECommersionAPI.Application.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException() : base("authentication error")
        {
        }

        public AuthenticationException(string? message) : base(message)
        {
        }

        protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
