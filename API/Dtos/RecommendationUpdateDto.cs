using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class RecommendationUpdateDto
    {
        public int PatientId { get; set; }
        [MaxLength(25)]
        public required string Type { get; set; }
        [MaxLength(256)]
        public required string Title { get; set; }
        [MaxLength(512)]
        public string? Memo { get; set; }
        public bool Completed { get; set;}
    }
}