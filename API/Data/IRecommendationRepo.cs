using API.Models;

namespace API.Data
{
    public interface IRecommendationRepo
    {
        Task SaveChanges();
        Task<IEnumerable<Recommendation>> GetAllRecommendations();
        Task<Recommendation?> GetRecommendationById(int id);
        Task CreateRecommedation(Recommendation rec);
        void DeleteRecommendation(Recommendation rec);
        Task<Patient?> GetPatientById(int id);
    }
}
