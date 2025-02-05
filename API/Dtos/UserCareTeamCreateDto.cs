using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class UserCareTeamCreateDto
    {
        [Required(ErrorMessage = "UserId is Required")]
        public required string UserId { get; set; }
        [Required(ErrorMessage = "CareTeamId is Required")]
        public int CareTeamId { get; set; }
    }
}
