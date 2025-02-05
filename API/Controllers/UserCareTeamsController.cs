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
    public class UserCareTeamsController : ControllerBase
    {
        private readonly IUserCareTeamRepo _repo;
        private readonly IMapper _mapper;

        public UserCareTeamsController(IUserCareTeamRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCareTeamReadDto>>> GetAllUserCareTeams([FromQuery] UserCareTeamGetByUserIdDto uctuiddto)
        {
            if (uctuiddto.userId == null)
            {
                var userCareTeams = await _repo.GetAllUserCareTeams();

                return Ok(_mapper.Map<IEnumerable<UserCareTeamReadDto>>(userCareTeams));
            }
            else
            {
                var userCareTeams = await _repo.GetByUserId(uctuiddto.userId);

                return Ok(_mapper.Map<IEnumerable<UserCareTeamReadDto>>(userCareTeams));
            }
        }

        [HttpGet("{id}", Name = "GetUserCareTeamById")]
        public async Task<ActionResult<UserCareTeamReadDto>> GetUserCareTeamById(int id)
        {
            var uct = await _repo.GetUserCareTeamById(id);
            if (uct != null)
            {
                return Ok(_mapper.Map<UserCareTeamReadDto>(uct));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<UserCareTeamReadDto>> CreateUserCareTeam(UserCareTeamCreateDto uctCreateDto)
        {
            var uctModel = _mapper.Map<UserCareTeam>(uctCreateDto);

            var user = await _repo.GetUserById(uctModel.UserId);
            if (user == null)
            {
                return ValidationProblem("No User found for UserId");
            }
            uctModel.User = user;

            var careTeam = await _repo.GetCareTeamById(uctModel.CareTeamId);
            if (careTeam == null)
            {
                return ValidationProblem("No CareTeam found for CareTeamId");
            }
            uctModel.CareTeam = careTeam;

            uctModel.CreatedAt = DateTime.UtcNow;
            uctModel.UpdatedAt = DateTime.UtcNow;

            await _repo.CreateUserCareTeam(uctModel);
            await _repo.SaveChanges();

            var uctReadDto = _mapper.Map<UserCareTeamReadDto>(uctModel);
            return CreatedAtRoute(nameof(GetUserCareTeamById), new { Id = uctReadDto.Id }, uctReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UserCareTeamUpdate(int id, UserCareTeamUpdateDto uctUpdateDto)
        {
            var uctModel = await _repo.GetUserCareTeamById(id);
            if (uctModel == null)
            {
                return NotFound();
            }

            var oldUserId = uctModel.UserId;
            var oldCareTeamId = uctModel.CareTeamId;

            _mapper.Map(uctUpdateDto, uctModel);

            if (oldUserId != uctModel.UserId)
            {
                var user = await _repo.GetUserById(uctModel.UserId);
                if (user == null)
                {
                    return ValidationProblem("No User found for UserId");
                }
                uctModel.User = user;
            }

            if (oldCareTeamId != uctModel.CareTeamId)
            {
                var careTeam = await _repo.GetCareTeamById(uctModel.CareTeamId);
                if (careTeam == null)
                {
                    return ValidationProblem("No CareTeam found for CareTeamId");
                }
                uctModel.CareTeam = careTeam;
            }

            uctModel.UpdatedAt = DateTime.UtcNow;

            await _repo.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> UserCareTeamDelete(int id)
        {
            var uctModel = await _repo.GetUserCareTeamById(id);
            if (uctModel == null)
            {
                return NotFound();
            }

            _repo.DeleteUserCareTeam(uctModel);
            await _repo.SaveChanges();
            return NoContent();
        }
    }
}
