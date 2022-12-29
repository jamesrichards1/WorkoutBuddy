namespace WorkoutBuddy.Data
{
    public class Exercise
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MinReps { get; set; }

        public int MaxReps { get; set; }

        public bool HasAmrap { get; set; }

        public bool HasWarmup { get; set; }

        public int Sets { get; set; }

        public RestTime? RestTime { get; set; }
    }
}
