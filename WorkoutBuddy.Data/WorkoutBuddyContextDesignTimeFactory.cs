using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WorkoutBuddy.Data
{
    public class WorkoutBuddyContextDesignTimeFactory : IDesignTimeDbContextFactory<WorkoutBuddyContext>
    {
        public WorkoutBuddyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WorkoutBuddyContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WorkoutBuddyDevelopment;Trusted_Connection=True");
            return new WorkoutBuddyContext(optionsBuilder.Options);
        }
    }
}
