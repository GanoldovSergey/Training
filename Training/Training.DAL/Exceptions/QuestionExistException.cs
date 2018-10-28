using System;

namespace Training.DAL.Exceptions
{
    public class QuestionExistException : Exception
    {
        public QuestionExistException(string message) : base(message)
        {
        }
    }
}