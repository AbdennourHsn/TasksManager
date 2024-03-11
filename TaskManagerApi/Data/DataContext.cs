using System;
using Microsoft.EntityFrameworkCore;
using TaskManager.Entities;
using TaskEntity = TaskManager.Entities.TaskEntity;

namespace TaskManager.Data
{
	public class DataContext : DbContext
	{
		public DbSet<User> users { get; set; }
		public DbSet<TaskEntity> tasks { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.Creator) // Specify the navigation property
                .WithMany() // User can have multiple tasks
                .HasForeignKey(t => t.CreatorId) // Define the foreign key property
                .IsRequired(true);

            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.AssignedUser) 
                .WithMany() 
                .HasForeignKey(t => t.AssignedUserId) 
                .IsRequired(false);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
            });
                
        }
    }
}

