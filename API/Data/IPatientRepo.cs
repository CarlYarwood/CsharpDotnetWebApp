using API.Models;

namespace API.Data
{
    public interface IPatientRepo
    {
        Task SaveChanges();
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient?> GetPatientById(int id);
        Task CreatePatient(Patient p);
        void DeletePatient(Patient p);
        Task<CareTeam?> GetCareTeamById(int id);
        Task<IEnumerable<Patient>> GetByCareTeamId(int? id);
    }
}
