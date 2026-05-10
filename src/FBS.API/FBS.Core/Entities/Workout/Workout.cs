using FBS.Core.Enums;

namespace FBS.Core.Entities.Training
{
    public class Workout : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public WorkoutType Type { get; set; } = WorkoutType.Other;
        public List<Exercise> Exercises { get; set; }
        public DateTime CreatedAt {  get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt {  get; set; } = DateTime.UtcNow;
    }
}
