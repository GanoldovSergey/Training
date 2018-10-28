using System.Collections.Generic;

namespace Training.WebApi.Entities
{
    public class TestEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<QuestionEntity> Questions { get; set; }
    }
}