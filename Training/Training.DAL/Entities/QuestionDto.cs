using Newtonsoft.Json;
using System.Collections.Generic;

namespace Training.DAL.Entities
{
    public class QuestionDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string Text { get; set; }

        public bool IsMultiple { get; set; }

        public ICollection<AnswerDto> Answers { get; set; }
    }
}
