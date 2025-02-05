using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class RecommendationRepo : IRecommendationRepo
    {
        private readonly AppDbContext _context;
        
        public RecommendationRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateRecommedation(Recommendation rec)
        {
            if (rec == null)
            {
                throw new ArgumentNullException(nameof(rec));
            }
            await _context.Recommendations.AddAsync(rec);
        }

        public void DeleteRecommendation(Recommendation rec)
        {
            if (rec == null)
            {
                throw new ArgumentNullException(nameof(rec));
            }
            _context.Recommendations.Remove(rec);
        }

        public async Task<IEnumerable<Recommendation>> GetAllRecommendations()
        {
            return await _context.Recommendations.ToListAsync();
        }

        public async Task<Patient?> GetPatientById(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Recommendation?> GetRecommendationById(int id)
        {
            return await _context.Recommendations.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}