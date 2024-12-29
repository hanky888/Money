using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Money.Models
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext() { }       
        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public virtual DbSet<Currency> Currency { get; set; }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)  
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CurrencyConnection"));
            }
        }  
    }
}
