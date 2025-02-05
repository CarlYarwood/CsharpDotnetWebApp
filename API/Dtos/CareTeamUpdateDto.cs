using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CareTeamUpdateDto
    {
        public string? LeadId { get; set; }
        [MaxLength(256)]
        public required string Name { get; set; }
    }
}