﻿namespace ECommersionAPI.Application.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException():base("username or paswword invalid")
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {
        }

        public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
