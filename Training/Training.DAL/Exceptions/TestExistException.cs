using System;

namespace Training.DAL.Exceptions
{
    public class TestExistException : Exception
    {
        public TestExistException(string message) : base(message)
        {
        }
    }
}