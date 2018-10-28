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
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IConverter<QuestionEntity, QuestionDto> _converter;

        public QuestionService(IQuestionRepository questionRepository, IConverterFactory converterFactory)
        {
            _questionRepository = questionRepository;
            _converter = converterFactory.GetConverter<QuestionEntity,QuestionDto>();
        }

        public async Task<QuestionResponse> CreateQuestionAsync(QuestionEntity question)
        {
            try
            {
                var response = Validators.ValidateQuestion(question);
                if (!response.Success) return response;

                await _questionRepository.CreateQuestionAsync(_converter.Convert(question));
                return new QuestionResponse { Success = true };
            }
            catch (QuestionExistException)
            {
                return new QuestionResponse { ErrorMessage =String.Format(Resource.ErrorMessageQuestionExists, question.Text), Success = false };
            }
        }

        public async Task DeleteQuestionAsync(string id)
        {
            await _questionRepository.DeleteQuestionAsync(id);
        }

        public async Task<QuestionResponse> UpdateQuestionAsync(string id, QuestionEntity question)
        {
            try
            {
                var response = Validators.ValidateQuestion(question);
                if (!response.Success) return response;

                await _questionRepository.UpdateQuestionAsync(id, _converter.Convert(question));
                return new QuestionResponse { Success = true };
            }
            catch (QuestionExistException)
            {
                return new QuestionResponse { ErrorMessage = String.Format(Resource.ErrorMessageQuestionExists, question.Text), Success = false };
            }
        }

        public async Task<QuestionEntity> GetQuestionByIdAsync(string id)
        {
            var result = await _questionRepository.GetQuestionByIdAsync(id);
            return _converter.Convert(result);
        }

        public async Task<IEnumerable<QuestionEntity>> GetQuestionsAsync()
        {
            var questions = await _questionRepository.GetQuestionsAsync();
            return questions?.Where(question => question != null)
                .Select(question => _converter.Convert(question));
        }

        public async Task<bool> IsQuestionExistAsync(QuestionEntity question)
        {
            return await _questionRepository.IsQuestionExistAsync(_converter.Convert(question));
        }
    }
}