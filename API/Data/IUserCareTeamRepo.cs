using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public interface IUserCareTeamRepo
    {
        Task SaveChanges();
        Task<IEnumerable<UserCareTeam>> GetAllUserCareTeams();
        Task<UserCareTeam?> GetUserCareTeamById(int id);
        Task CreateUserCareTeam(UserCareTeam uct);
        void DeleteUserCareTeam(UserCareTeam uct);
        Task<IdentityUser?> GetUserById(string id);
        Task<CareTeam?> GetCareTeamById(int id);
        Task<IEnumerable<UserCareTeam>> GetByUserId(string id);
    }
}
