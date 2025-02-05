using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Index(nameof(PatientId))]
    public class Recommendation
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "PatientId is Required")]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual required Patient Patient { get; set; }
        [Required(ErrorMessage = "Recommendation Type is Required")]
        [MaxLength(25)]
        public required string Type { get; set; }
        [Required(ErrorMessage = "Title is Required")]
        [MaxLength(256)]
        public required string Title { get; set; }
        [MaxLength(512)]
        public string? Memo { get; set; }
        public bool Completed { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}