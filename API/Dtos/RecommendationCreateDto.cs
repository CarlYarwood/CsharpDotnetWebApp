using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class RecommendationCreateDto
    {
        [Required(ErrorMessage = "PatientId is Required")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Recommendation Type is Required")]
        [MaxLength(25)]
        public required string Type { get; set; }
        [Required(ErrorMessage = "Title is Required")]
        [MaxLength(256)]
        public required string Title { get; set; }
        [MaxLength(512)]
        public string? Memo { get; set; }
        public bool Completed { get; set;}
    }
}