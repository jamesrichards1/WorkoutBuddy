namespace WorkoutBuddy.Data
{
    public class WorkoutSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int WorkoutId {get; set;}
        public Workout Workout { get; set; }
    }
}
