using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Index(nameof(CareTeamId))]
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(35)]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is Required")]
        [MaxLength(35)]
        public required string LastName { get; set; }
        [Required(ErrorMessage = "CareTeamId is Required")]
        [ForeignKey("CareTeam")]
        public int CareTeamId { get; set; }
        public virtual required CareTeam CareTeam { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}