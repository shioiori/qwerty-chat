using System.ComponentModel.DataAnnotations;

namespace qwerty_chat_api.DTOs
{
    public class LogRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Name { get; set; }
        public bool? Gender { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public string? CoverPhoto { get; set; }
    }
}
