using System.Runtime.Serialization;

namespace ECommersionAPI.Application.Exceptions
{
    public class UserCreatefailedException : Exception
    {
        public UserCreatefailedException():base("Something went worg :)")
        {
        }

        public UserCreatefailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserCreatefailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
