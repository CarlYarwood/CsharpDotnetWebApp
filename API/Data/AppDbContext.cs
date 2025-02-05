using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace API.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) {}
        public DbSet<CareTeam> CareTeams => Set<CareTeam>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Recommendation> Recommendations => Set<Recommendation>();
        public DbSet<UserCareTeam> UserCareTeams => Set<UserCareTeam>();
    }
}