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
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepo _repo;
        private readonly IMapper _mapper;

        public PatientsController(IPatientRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientReadDto>>> GetAllPatients(PatientByCareTeamIdDto pbctidDto)
        {
            if (pbctidDto.careTeamId != null)
            {
                var patients = await _repo.GetByCareTeamId(pbctidDto.careTeamId);

                return Ok(_mapper.Map<IEnumerable<PatientReadDto>>(patients));
            }
            else
            {
                var patients = await _repo.GetAllPatients();

                return Ok(_mapper.Map<IEnumerable<PatientReadDto>>(patients));
            }
        }

        [HttpGet("{id}", Name = "GetPatientById")]
        public async Task<ActionResult<PatientReadDto>> GetPatientById(int id)
        {
            var patient = await _repo.GetPatientById(id);
            if (patient != null)
            {
                return Ok(_mapper.Map<PatientReadDto>(patient));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PatientReadDto>> createPatient(PatientCreateDto patientCreateDto)
        {
            var patientModel = _mapper.Map<Patient>(patientCreateDto);
            var careTeam = await _repo.GetCareTeamById(patientModel.CareTeamId);
            if (careTeam == null)
            {
                return ValidationProblem("No CareTeam found for CareTeamId");
            }
            patientModel.CareTeam = careTeam;
            patientModel.CreatedAt = DateTime.UtcNow;
            patientModel.UpdatedAt = DateTime.UtcNow;

            await _repo.CreatePatient(patientModel);
            await _repo.SaveChanges();

            var patientReadDto = _mapper.Map<PatientReadDto>(patientModel);

            return CreatedAtRoute(nameof(GetPatientById), new { Id = patientReadDto.Id}, patientReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PatientUpdate(int id, PatientUpdateDto patientUpdateDto)
        {
            var patientModel = await _repo.GetPatientById(id);
            if (patientModel == null)
            {
                return NotFound();
            }
            
            var oldCareTeamId = patientModel.CareTeamId;

            _mapper.Map(patientUpdateDto, patientModel);
            if (oldCareTeamId != patientModel.CareTeamId)
            {
                var careTeam = await _repo.GetCareTeamById(patientModel.CareTeamId);
                if (careTeam == null)
                {
                    return ValidationProblem("No CareTeam found for CareTeamId");
                }
                patientModel.CareTeam = careTeam;
            }
            patientModel.UpdatedAt = DateTime.UtcNow;

            await _repo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var patientModel = await _repo.GetPatientById(id);
            if (patientModel == null)
            {
                return NotFound();
            }

            _repo.DeletePatient(patientModel);
            await _repo.SaveChanges();
            return NoContent();
        }
    }
}
