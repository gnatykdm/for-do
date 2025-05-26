using Microsoft.EntityFrameworkCore;
using Enums;
using Model;

namespace ForDoListApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
        public DbSet<PriorityEntity> Priorities => Set<PriorityEntity>();
        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();
        public DbSet<TaskHistoryEntity> TaskHistories => Set<TaskHistoryEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresEnum<TaskColor>();
            modelBuilder.HasPostgresEnum<Enums.TaskStatus>();

            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.User)
                .WithMany(u => u.UserTasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.Priority)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.PriorityId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskHistoryEntity>()
                .HasOne(th => th.Task)
                .WithMany(t => t.TaskHistories)
                .HasForeignKey(th => th.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskHistoryEntity>()
                .HasOne(th => th.User)
                .WithMany(u => u.TaskHistories)
                .HasForeignKey(th => th.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
