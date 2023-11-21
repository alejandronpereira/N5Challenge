using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    public class N5DbContext : DbContext
    {
        public N5DbContext(DbContextOptions<N5DbContext> options) : base(options) { }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<PermissionType> PermissionType { get; set; }
    }
}
