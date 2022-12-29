namespace WorkoutBuddy.Data
{
    public class Sets
    {
        public int Id { get; set; }

        public int ExerciseSessionId { get; set; }

        public ExerciseSession ExerciseSession { get; set; }

        public int Reps { get; set; }

        public SetType SetType { get; set; }
    }
}
