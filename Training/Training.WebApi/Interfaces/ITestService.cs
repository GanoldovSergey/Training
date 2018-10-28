using System.Collections.Generic;
using System.Threading.Tasks;
using Training.WebApi.Entities;
using Training.WebApi.Entities.Responses;

namespace Training.WebApi.Interfaces
{
    public interface ITestService
    {
        Task<TestResponse> CreateTestAsync(TestEntity test);

        Task DeleteTestAsync(string id);

        Task<TestEntity> GetTestByIdAsync(string id);

        Task<IEnumerable<TestEntity>> GetTestsAsync();

        Task<TestResponse> UpdateTestAsync(string id, TestEntity test);

        Task<bool> IsTestExistAsync(TestEntity test);

        Task AddQuestionToTest(string testId, QuestionEntity question);

        Task DelQuestionFromTest(string testId, QuestionEntity question);
    }
}