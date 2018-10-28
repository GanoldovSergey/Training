using System.Collections.Generic;
using System.Threading.Tasks;
using Training.DAL.Entities;

namespace Training.DAL.Services.Interfaces
{
    public interface IQuestionRepository
    {
        Task CreateQuestionAsync(QuestionDto question);

        Task DeleteQuestionAsync(string id);

        Task<QuestionDto> GetQuestionByIdAsync(string id);

        Task<IEnumerable<QuestionDto>> GetQuestionsAsync();

        Task UpdateQuestionAsync(string id, QuestionDto question);

        Task<bool> IsQuestionExistAsync(QuestionDto question);
    }
}
