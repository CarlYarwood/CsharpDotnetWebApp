using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace API.Models
{
    [Index(nameof(UserId))]
    public class UserCareTeam
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "UserId is Required")]
        [ForeignKey("User")]
        [MaxLength(450)]
        public required string UserId { get; set; }
        public virtual required IdentityUser User {get; set; }
        [Required(ErrorMessage = "CareTeamId is Required")]
        [ForeignKey("CareTeam")]
        public int CareTeamId { get; set; }
        public virtual required CareTeam CareTeam { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}