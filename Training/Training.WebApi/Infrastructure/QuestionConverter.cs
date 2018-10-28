using System.Linq;
using Training.DAL.Entities;
using Training.WebApi.Entities;
using Training.WebApi.Interfaces;

namespace Training.WebApi.Infrastructure
{
    public class QuestionConverter : IConverter<QuestionEntity, QuestionDto>
    {
        private readonly AnswerConverter _converter = new AnswerConverter();

        public QuestionDto Convert(QuestionEntity question)
        {
            return new QuestionDto
            {
                Id = question?.Id,
                Text = question?.Text,
                IsMultiple = question == null ? false : question.IsMultiple,
                Answers = question?.Answers.Select(a => _converter.Convert(a)).ToList()
            };
        }

        public QuestionEntity Convert(QuestionDto question)
        {
            return new QuestionEntity
            {
                Id = question?.Id,
                Text = question?.Text,
                IsMultiple = question == null ? false : question.IsMultiple,
                Answers = question?.Answers.Select(a => _converter.Convert(a)).ToList()
            };
        }
    }
}