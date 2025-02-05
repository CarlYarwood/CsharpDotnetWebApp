namespace API.Dtos
{
    public class UserCareTeamReadDto
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public int CareTeamId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
