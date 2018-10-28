using System.Linq;
using Training.DAL.Entities;
using Training.WebApi.Entities;
using Training.WebApi.Interfaces;

namespace Training.WebApi.Infrastructure
{
    public class TestConverter : IConverter<TestEntity, TestDto>
    {
        private readonly QuestionConverter _converter = new QuestionConverter();

        public TestDto Convert(TestEntity test)
        {
            return new TestDto
            {
                Id = test?.Id,
                Name = test?.Name,
                Questions = test?.Questions?.Select(q => _converter.Convert(q)).ToList()
            };
        }

        public TestEntity Convert(TestDto test)
        {
            return new TestEntity
            {
                Id = test?.Id,
                Name = test?.Name,
                Questions = test?.Questions?.Select(q => _converter.Convert(q)).ToList()
            };
        }
    }
}