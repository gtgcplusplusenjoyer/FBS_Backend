namespace FBS.Core.Entities.Training
{
    public class Training : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public List<Exercise> Exercises { get; set; }
        public DateTime CreatedAt {  get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt {  get; set; } = DateTime.UtcNow;
    }
}
