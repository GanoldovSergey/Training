using Newtonsoft.Json;
using System.Collections.Generic;

namespace Training.DAL.Entities
{
    public class TestDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<QuestionDto> Questions { get; set; }
    }
}
