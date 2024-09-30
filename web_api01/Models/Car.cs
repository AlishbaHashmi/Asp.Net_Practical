namespace web_api01.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
