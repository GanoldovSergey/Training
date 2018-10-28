using System.Collections.Generic;
using System.Linq;
using Training.WebApi.Entities;
using System.Threading.Tasks;
using Training.WebApi.Interfaces;
using Training.WebApi.Entities.Responses;
using System;
using Training.DAL.Exceptions;
using Training.DAL.Services.Interfaces;
using Training.DAL.Entities;

namespace Training.WebApi.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IConverter<TestEntity, TestDto> _converter;

        public TestService(ITestRepository testRepository, IConverterFactory converterFactory)
        {
            _testRepository = testRepository;
            _converter = converterFactory.GetConverter<TestEntity, TestDto>();
        }

        public async Task<TestResponse> CreateTestAsync(TestEntity test)
        {
            try
            {
                await _testRepository.CreateTestAsync(_converter.Convert(test));
                return new TestResponse { Success = true };
            }
            catch (TestExistException)
            {
                return new TestResponse { ErrorMessage = String.Format(Resource.ErrorMessageTestExists, test.Name), Success = false };
            }
        }

        public async Task DeleteTestAsync(string id)
        {
            await _testRepository.DeleteTestAsync(id);
        }

        public async Task<TestResponse> UpdateTestAsync(string id, TestEntity test)
        {
            try
            {
                await _testRepository.UpdateTestAsync(id, _converter.Convert(test));
                return new TestResponse { Success = true };
            }
            catch (TestExistException)
            {
                return new TestResponse { ErrorMessage = $"Test with Name \"{test.Name}\" is already exist!", Success = false };
            }
        }

        public async Task<TestEntity> GetTestByIdAsync(string id)
        {
            var result = await _testRepository.GetTestByIdAsync(id);
            return _converter.Convert(result);
        }

        public async Task<IEnumerable<TestEntity>> GetTestsAsync()
        {
            var tests = await _testRepository.GetTestsAsync();
            return tests?.Where(test => test != null)
                .Select(test => _converter.Convert(test));
        }

        public async Task<bool> IsTestExistAsync(TestEntity test)
        {
            return await _testRepository.IsTestExistAsync(_converter.Convert(test));
        }

        public async Task AddQuestionToTest(string testId, QuestionEntity question)
        {
            var test = await GetTestByIdAsync(testId);
            test.Questions.Add(question);

            await UpdateTestAsync(testId, test);
        }

        public async Task DelQuestionFromTest(string testId, QuestionEntity question)
        {
            var test = await GetTestByIdAsync(testId);
            test.Questions.Remove(question);

            await UpdateTestAsync(testId, test);
        }
    }
}