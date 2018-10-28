using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Training.WebApi.Entities;
using Training.WebApi.Interfaces;

namespace Training.WebApi.Controllers
{
    public class TestController : ApiController
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public async Task<IEnumerable<TestEntity>> IndexAsync()
        {
            return await _testService.GetTestsAsync();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateAsync(TestEntity test)
        {
            var result = await _testService.CreateTestAsync(test);
            return (result.Success) ? Request.CreateResponse(HttpStatusCode.OK) :
                Request.CreateResponse(HttpStatusCode.InternalServerError, result.ErrorMessage);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> EditAsync(TestEntity test)
        {
            var result = await _testService.UpdateTestAsync(test.Id, test);
            return (result.Success) ? Request.CreateResponse(HttpStatusCode.OK) :
                Request.CreateResponse(HttpStatusCode.InternalServerError, result.ErrorMessage);
        }

        public async Task DeleteAsync(string id)
        {
            await _testService.DeleteTestAsync(id);
        }

        [HttpPost]
        public async Task AddQuestionAsync(string testId, QuestionEntity question)
        {
            await _testService.AddQuestionToTest(testId, question);
        }

        public async Task DeleteQuestionAsync(string testId, QuestionEntity question)
        {
            await _testService.DelQuestionFromTest(testId, question);
        }
    }
}
