using Microsoft.EntityFrameworkCore;
using Models.Entity;
using Models.Entity.Enums;

namespace ForDoListApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresEnum<TaskStatusEnum>();
            modelBuilder.HasPostgresEnum<TaskPriorityEnum>();
            modelBuilder.HasPostgresEnum<TaskCategoryEnum>();

            // Configure UserEntity
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.UserId)
                      .UseIdentityByDefaultColumn();

                entity.Property(u => u.UserName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.PasswordHash)
                      .IsRequired();

                entity.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasMany(u => u.UserTasks)
                      .WithOne(t => t.User)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure TaskEntity
            modelBuilder.Entity<TaskEntity>(entity =>
            {
                entity.HasKey(t => t.TaskId);

                entity.Property(t => t.TaskId).UseIdentityByDefaultColumn();

                entity.Property(t => t.TaskTitle)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(t => t.TaskDescription)
                    .HasMaxLength(1000);

                entity.Property(t => t.CreatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(t => t.UpdatedAt)
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(t => t.DueDate)
                    .HasColumnType("timestamp with time zone");

                entity.Property(t => t.Status)
                    .HasConversion<string>();

                entity.Property(t => t.Priority)
                    .HasConversion<string>();

                entity.Property(t => t.Category)
                    .HasConversion<string>();

                entity.HasOne(t => t.User)
                    .WithMany(u => u.UserTasks)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(t => t.UserId);
            });
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<TaskEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow; 
                }
            }
            return base.SaveChanges();
        }
    }
}
