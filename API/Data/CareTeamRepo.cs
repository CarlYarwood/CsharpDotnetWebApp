using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data 
{
    public class CareTeamRepo : ICareTeamRepo
    {
        private readonly AppDbContext _context;
        public CareTeamRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateCareTeam(CareTeam ct)
        {
            if (ct == null)
            {
                throw new ArgumentNullException(nameof(ct));
            }

            await _context.CareTeams.AddAsync(ct);
        }

        public void DeleteCareTeam(CareTeam ct)
        {
            if (ct == null)
            {
                throw new ArgumentNullException(nameof(ct));
            }

            _context.CareTeams.Remove(ct);
        }

        public async Task<IEnumerable<CareTeam>> GetAllCareTeams()
        {
            return await _context.CareTeams.ToListAsync();
        }

        public async Task<CareTeam?> GetCareTeamById(int id)
        {
            return await _context.CareTeams.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IdentityUser?> GetLeadById(string? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}