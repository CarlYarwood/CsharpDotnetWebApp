using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PatientRepo : IPatientRepo
    {
        private readonly AppDbContext _context;

        public PatientRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreatePatient(Patient p)
        {
            if (p == null)
            {
                throw new ArgumentException(nameof(p));
            }

            await _context.Patients.AddAsync(p);
        }

        public void DeletePatient(Patient p)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }

            _context.Patients.Remove(p);
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<IEnumerable<Patient>> GetByCareTeamId(int? id)
        {
            if (id == null)
            {
                return await _context.Patients.ToListAsync();
            }
            return await _context.Patients.Where(p => p.CareTeamId == id).ToListAsync();
        }

        public async Task<CareTeam?> GetCareTeamById(int id)
        {
            return await _context.CareTeams.FirstOrDefaultAsync(ct => ct.Id == id);
        }

        public async Task<Patient?> GetPatientById(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}