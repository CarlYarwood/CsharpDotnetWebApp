using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using API.Models;
using API.Dtos;
using API.Data;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CareTeamsController : ControllerBase
    {
        private readonly ICareTeamRepo _repo;
        private readonly IMapper _mapper;

        public CareTeamsController(ICareTeamRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper =  mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CareTeamReadDto>>> GetAllCareTeams()
        {
            var careTeams = await _repo.GetAllCareTeams();

            return Ok(_mapper.Map<IEnumerable<CareTeamReadDto>>(careTeams));
        }

        [HttpGet("{id}", Name = "GetCareTeamById")]
        public async Task<ActionResult<CareTeamReadDto>> GetCareTeamById(int id)
        {
            var careTeam = await _repo.GetCareTeamById(id);
            if (careTeam != null) 
            {
                return Ok(_mapper.Map<CareTeamReadDto>(careTeam));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CareTeamReadDto>> createCareTeam(CareTeamCreateDto ctCreateDto)
        {
            var careTeamModel = _mapper.Map<CareTeam>(ctCreateDto);

            careTeamModel.Lead = await _repo.GetLeadById(careTeamModel.LeadId);
            careTeamModel.CreatedAt = DateTime.UtcNow;
            careTeamModel.UpdatedAt = DateTime.UtcNow;

            await _repo.CreateCareTeam(careTeamModel);
            await _repo.SaveChanges();

            var ctReadDto = _mapper.Map<CareTeamReadDto>(careTeamModel);

            return CreatedAtRoute(nameof(GetCareTeamById), new { Id = ctReadDto.Id}, ctReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> CareTeamUpdate(int id, CareTeamUpdateDto careTeamUpdateDto)
        {
            var careTeamModel = await _repo.GetCareTeamById(id);

            if (careTeamModel == null)
            {
                return NotFound();
            }

            var oldLeadId = careTeamModel.LeadId;

            _mapper.Map(careTeamUpdateDto, careTeamModel);
            if (oldLeadId != careTeamModel.LeadId)
            {
                careTeamModel.Lead = await _repo.GetLeadById(careTeamModel.LeadId);
            }
            careTeamModel.UpdatedAt = DateTime.UtcNow;

            await _repo.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public  async Task<ActionResult> DeleteCareTeam(int id)
        {
            var careTeamModel = await _repo.GetCareTeamById(id);

            if (careTeamModel == null)
            {
                return NotFound();
            }
            
            careTeamModel.Lead = null;
            _repo.DeleteCareTeam(careTeamModel);
            await _repo.SaveChanges();
            return NoContent();
        }
    }
}
