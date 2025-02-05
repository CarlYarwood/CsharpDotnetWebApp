namespace API.Dtos
{
    public class RecommendationReadDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public required  string Type { get; set; }
        public required string Title { get; set; }
        public string? Memo { get; set; }
        public bool Completed { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}