using HaberApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HaberApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext()
        {
        }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
        
        public DbSet<Admin> Adminler { get; set; }
        public DbSet<Haber> Haberler { get; set; }
    }
}
