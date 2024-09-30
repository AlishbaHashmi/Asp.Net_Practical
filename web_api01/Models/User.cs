using System.ComponentModel.DataAnnotations;

namespace web_api01.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
