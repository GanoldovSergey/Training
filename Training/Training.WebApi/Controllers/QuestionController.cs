using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Training.WebApi.Entities;
using Training.WebApi.Interfaces;

namespace Training.WebApi.Controllers
{
    public class QuestionController : ApiController
    {
        private IQuestionService _service;

        public QuestionController(IQuestionService service)
        {
            _service = service;
        }

        // GET api/<controller>
        public async Task<IEnumerable<QuestionEntity>> GetQuestions()
        {
            return await _service.GetQuestionsAsync();
        }

        // GET api/<controller>/5
        public async Task<QuestionEntity> GetQuestion(string id)
        {
            return await _service.GetQuestionByIdAsync(id);

        }

        [HttpPost]
        // POST api/<controller>
        public async Task<HttpResponseMessage> CreateQuestion([FromBody]QuestionEntity question)
        {
            var result = await _service.CreateQuestionAsync(question);
            return (result.Success) ? Request.CreateResponse(HttpStatusCode.OK) :
                Request.CreateResponse(HttpStatusCode.InternalServerError, result.ErrorMessage);
        }

        [HttpPut]
        // PUT api/<controller>/5
        public async Task<HttpResponseMessage> EditQuestion([FromBody]QuestionEntity question)
        {
            var result = await _service.UpdateQuestionAsync(question.Id, question);
            return (result.Success) ? Request.CreateResponse(HttpStatusCode.OK) :
    Request.CreateResponse(HttpStatusCode.InternalServerError, result.ErrorMessage);

        }

        // DELETE api/<controller>/5
        public async Task DeleteQuestion(QuestionEntity question)
        {
            await _service.DeleteQuestionAsync(question.Id);
        }
    }
}