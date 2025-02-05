using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class PatientUpdateDto
    {
        [MaxLength(35)]
        public required string FirstName { get; set; }
        [MaxLength(35)]
        public required string LastName { get; set; }
        public int CareTeamId { get; set; }
    }
}