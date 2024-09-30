using web_api01.Models;

using Microsoft.EntityFrameworkCore;

namespace web_api01.Data
{
    public class VehicleContext : DbContext
    {

        public VehicleContext(DbContextOptions<VehicleContext> options)
            : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Car> Cars { get; set; }
    }

 }