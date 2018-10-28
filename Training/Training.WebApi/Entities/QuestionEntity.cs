using System.Collections.Generic;

namespace Training.WebApi.Entities
{
    public class QuestionEntity
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public bool IsMultiple { get; set; }

        public ICollection<AnswerEntity> Answers { get; set; }
    }
}