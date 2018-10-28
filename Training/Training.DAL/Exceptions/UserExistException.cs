using System;

namespace Training.DAL.Exceptions
{
    public class UserExistException: Exception
    {
        public UserExistException(string message): base(message)
        {
        }
    }
}
