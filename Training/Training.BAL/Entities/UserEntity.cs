using System.ComponentModel.DataAnnotations;

namespace Training.BAL.Entities
{
    public class UserEntity
    {
        private const string ErrMsgNameRequired = "Name is requiered";
        private const string ErrMsgNameLength = "Name's length must be from 3 to 15 symbols";
        private const string ErrMsgPasswordLength = "Password's length must be from 3 to 15 symbols";
        private const string ErrMsgPasswordRequired = "Password is requiered";

        public string Id { get; set; }
        [Required(ErrorMessage = ErrMsgNameRequired)]
        [MaxLength(15, ErrorMessage = ErrMsgNameLength)]
        [MinLength(3, ErrorMessage = ErrMsgNameLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrMsgPasswordRequired)]
        [MaxLength(15, ErrorMessage = ErrMsgPasswordLength)]
        [MinLength(3, ErrorMessage = ErrMsgPasswordLength)]
        public string Password { get; set; }

        public Roles Role { get; set; }
    }
}
