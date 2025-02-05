using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using API.Models;
using API.Dtos;
using API.Data;
using API.Roles.Utils;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationRepo _repo;
        private readonly IMapper _mapper;
        private readonly HashSet<string> _types;

        public RecommendationsController(IRecommendationRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _types = Sets.RecommendationTypes;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecommendationReadDto>>> GetAllRecommendations()
        {
            var recs = await _repo.GetAllRecommendations();
            return Ok(_mapper.Map<IEnumerable<RecommendationReadDto>>(recs));
        }

        [HttpGet("{id}", Name = "GetRecommendationById")]
        public async Task<ActionResult<RecommendationReadDto>> GetRecommendationById(int id)
        {
            var rec = await _repo.GetRecommendationById(id);
            if (rec != null)
            {
                return Ok(_mapper.Map<RecommendationReadDto>(rec));
            }
            
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<RecommendationReadDto>> CreateRecommendation(RecommendationCreateDto recCreateDto)
        {
            var recModel = _mapper.Map<Recommendation>(recCreateDto);

            if (!_types.Contains(recModel.Type))
            {
                return ValidationProblem("Type must be ALLERGY_CHECK, SCREENING, FOLLOW_UPPERSCRIPTION_CONSULTATION, or OTHER");
            }

            var patient = await _repo.GetPatientById(recModel.PatientId);
            if (patient == null)
            {
                return ValidationProblem("No Patient found for PatientId");
            }
            recModel.Patient = patient;

            recModel.CreatedAt = DateTime.UtcNow;
            recModel.UpdatedAt = DateTime.UtcNow;

            await _repo.CreateRecommedation(recModel);
            await _repo.SaveChanges();

            var recReadDto = _mapper.Map<RecommendationReadDto>(recModel);

            return CreatedAtRoute(nameof(GetRecommendationById), new { Id = recReadDto.Id }, recReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> RecommendationUpdate(int id, RecommendationUpdateDto recUpdateDto)
        {
            var recModel = await _repo.GetRecommendationById(id);
            if (recModel == null)
            {
                return NotFound();
            }

            var oldPatientId = recModel.PatientId;

            _mapper.Map(recUpdateDto, recModel);

            if (!_types.Contains(recModel.Type))
            {
                ValidationProblem("type must be ALLERGY_CHECK, SCREENING, FOLLOW_UPPERSCRIPTION_CONSULTATION, or OTHER");
            }

            if (oldPatientId != recModel.PatientId)
            {
                var patient = await _repo.GetPatientById(recModel.PatientId);
                if (patient == null)
                {
                    return ValidationProblem("No Patient found for PatientId");
                }
                recModel.Patient = patient;
            }

            recModel.UpdatedAt = DateTime.UtcNow;

            await _repo.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecommendation(int id)
        {
            var recModel = await _repo.GetRecommendationById(id);
            if (recModel == null)
            {
                return NotFound();
            }

            _repo.DeleteRecommendation(recModel);
            await _repo.SaveChanges();
            return NoContent();
        }
    }
}
