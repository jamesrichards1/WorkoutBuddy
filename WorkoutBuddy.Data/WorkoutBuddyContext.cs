using Microsoft.EntityFrameworkCore;

namespace WorkoutBuddy.Data
{
    public class WorkoutBuddyContext : DbContext
    {
        public WorkoutBuddyContext(DbContextOptions options) : base(options)
        {
        }

        public WorkoutBuddyContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutSession> WorkoutSessions { get; set; }
        public DbSet<ExerciseSession> ExerciseSessions { get; set; }
        public DbSet<Set> Sets { get; set; }
    }
}
