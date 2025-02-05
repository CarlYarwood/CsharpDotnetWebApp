using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CareTeamCreateDto
    {
        public string? LeadId { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(256)]
        public required string Name { get; set; }
    }
}