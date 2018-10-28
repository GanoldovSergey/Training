using System.Collections.Generic;
using System.Threading.Tasks;
using Training.WebApi.Entities;
using Training.WebApi.Entities.Responses;

namespace Training.WebApi.Interfaces
{
    public interface IQuestionService
    {
        Task<QuestionResponse> CreateQuestionAsync(QuestionEntity question);

        Task DeleteQuestionAsync(string id);

        Task<QuestionEntity> GetQuestionByIdAsync(string id);

        Task<IEnumerable<QuestionEntity>> GetQuestionsAsync();

        Task<QuestionResponse> UpdateQuestionAsync(string id, QuestionEntity question);

        Task<bool> IsQuestionExistAsync(QuestionEntity question);
    }
}