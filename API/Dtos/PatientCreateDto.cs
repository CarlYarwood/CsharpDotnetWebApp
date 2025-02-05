using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class PatientCreateDto
    {
        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(35)]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is Required")]
        [MaxLength(35)]
        public required string LastName { get; set; }
        [Required(ErrorMessage = "CareTeamId is Required")]
        public int CareTeamId { get; set; }
    }
}