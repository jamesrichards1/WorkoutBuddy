using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WorkoutBuddy.Data
{
    public class WorkoutBuddyContext : DbContext, IConfigurationDbContext, IPersistedGrantDbContext
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

        // Tables for Identity Server
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }
        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<ApiScope> ApiScopes { get; set; }
        public DbSet<IdentityProvider> IdentityProviders { get; set; }
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
        public DbSet<Key> Keys { get; set; }
        public DbSet<ServerSideSession> ServerSideSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceFlowCodes>().HasNoKey();
        }
    }

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
