using System;

namespace Training.DAL.Exceptions
{
    public class AnswerExistException : Exception
    {
        public AnswerExistException(string message) : base(message)
        {
        }
    }
}
