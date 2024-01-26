using System;

namespace newProject.Exceptions
{
    public class ForbbidenAccessException : Exception
    {
        public ForbbidenAccessException(string message) : base(message)
        {
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}