using System.ComponentModel.DataAnnotations;

namespace web_api01.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual Car Cars { get; set; }
    }
}
