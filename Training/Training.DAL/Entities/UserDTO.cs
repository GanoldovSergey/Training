using Newtonsoft.Json;

namespace Training.DAL.Entities
{
    public class UserDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public string Password { get; set; }

        public int Role { get; set; }
    }
}
