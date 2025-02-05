using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserCareTeamRepo : IUserCareTeamRepo
    {
        private readonly AppDbContext _context;
        
        public UserCareTeamRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserCareTeam(UserCareTeam uct)
        {
            if(uct == null)
            {
                throw new ArgumentNullException(nameof(uct));
            }

            await _context.UserCareTeams.AddAsync(uct);
        }

        public void DeleteUserCareTeam(UserCareTeam uct)
        {
            if (uct == null)
            {
                throw new ArgumentNullException(nameof(uct));
            }

            _context.UserCareTeams.Remove(uct);
        }

        public async Task<IEnumerable<UserCareTeam>> GetAllUserCareTeams()
        {
            return await _context.UserCareTeams.ToListAsync();
        }

        public async Task<IEnumerable<UserCareTeam>> GetByUserId(string id)
        {
            return await _context.UserCareTeams.Where(uct => uct.UserId == id).ToListAsync();
        }

        public async Task<CareTeam?> GetCareTeamById(int id)
        {
            return await _context.CareTeams.FirstOrDefaultAsync(ct => ct.Id == id);
        }

        public async Task<IdentityUser?> GetUserById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserCareTeam?> GetUserCareTeamById(int id)
        {
            return await _context.UserCareTeams.FirstOrDefaultAsync(uct => uct.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}