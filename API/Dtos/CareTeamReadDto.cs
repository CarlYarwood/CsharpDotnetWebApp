namespace API.Dtos
{
    public class CareTeamReadDto
    {
        public int Id { get; set; }
        public string? LeadId { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }
}
