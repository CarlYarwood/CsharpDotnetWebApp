using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public interface ICareTeamRepo 
    {
        Task SaveChanges();
        Task <IEnumerable<CareTeam>> GetAllCareTeams();
        Task <CareTeam?> GetCareTeamById(int id);
        Task CreateCareTeam(CareTeam ct);
        void DeleteCareTeam(CareTeam ct);
        Task <IdentityUser?> GetLeadById(string? id);
    }
}
