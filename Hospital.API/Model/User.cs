using System.ComponentModel.DataAnnotations;
namespace Hospital.API.Model
{
    public class User : LoginModel
    {
        public int Id { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Role { get; set; }
    }
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}