using System.ComponentModel.DataAnnotations;

namespace ScratchApp.API.dto
{
    public class UserForRegDto
    {
        [Required]
        public string username { get; set; }

        [Required]
        [StringLength(8,MinimumLength =5,ErrorMessage ="specify password between 5 and 8 characters")]
        public string password { get; set; }
    }
}