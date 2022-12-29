namespace WorkoutBuddy.Data
{
    public class ExerciseSession
    {
        public int Id { get; set; }

        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        public int WorkoutSessionId { get; set; }

        public WorkoutSession WorkoutSession { get; set; }

        public double Weight { get; set; }

        public List<Sets> Sets { get; set; }
    }
}
