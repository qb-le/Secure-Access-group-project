using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<AccessMethod> AccessMethods { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<QRToken> QRTokens { get; set; }

    }
}
