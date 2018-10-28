using Newtonsoft.Json;

namespace Training.DAL.Entities
{
    public class AnswerDto
    {
        public string Text { get; set; }

        public bool IsRight { get; set; }
    }
}
