using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Index(nameof(LeadId))]
    public class CareTeam
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("AspNetUsers")]
        public string? LeadId { get; set; }
        public virtual IdentityUser? Lead { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(256)]
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }
}