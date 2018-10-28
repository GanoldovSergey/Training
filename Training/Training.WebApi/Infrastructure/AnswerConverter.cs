using Training.DAL.Entities;
using Training.WebApi.Entities;
using Training.WebApi.Interfaces;

namespace Training.WebApi.Infrastructure
{
    public class AnswerConverter : IConverter<AnswerEntity, AnswerDto>
    {
        public AnswerDto Convert(AnswerEntity answer)
        {
            return new AnswerDto
            {
                Text = answer?.Text,
                IsRight = answer == null ? false : answer.IsRight
            };
        }

        public AnswerEntity Convert(AnswerDto answer)
        {
            return new AnswerEntity
            {
                Text = answer?.Text,
                IsRight = answer == null ? false : answer.IsRight
            };
        }
    }
}