using System.Linq;
using Training.WebApi.Entities;
using Training.WebApi.Entities.Responses;

namespace Training.WebApi.Services
{
    public static class Validators
    {
        public static QuestionResponse ValidateQuestion(QuestionEntity question)
        {
            if (question == null) return new QuestionResponse { Success = false, ErrorMessage = Resource.ErrorMessageQuestionIsNull };
            var numberOfRightAnswer = question.Answers.Count(x => x.IsRight);

            if (numberOfRightAnswer == 0) return new QuestionResponse { Success = false, ErrorMessage = Resource.ErrorMessageNoRightAnswers };
            if (numberOfRightAnswer > 1 && !question.IsMultiple) return new QuestionResponse { Success = false, ErrorMessage = Resource.ErrorMessageOnlyOneAnswerMayBeTrue };
            return new QuestionResponse { Success = true };
        }
    }
}