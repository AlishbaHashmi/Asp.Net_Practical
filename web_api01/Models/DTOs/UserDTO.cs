namespace web_api01.Models.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public int RoleId { get; set; }
    }
}
