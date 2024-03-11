using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Entities
{
	public class TaskEntity
	{
		[Key]
		public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [MaxLength(100)]
        public string Discription { get; set; }

		public TaskStatut Statut { get; set; } = TaskStatut.open;
		public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [Range(typeof(DateTime), "2024-01-01", "9999-12-31")]
        public DateTime DeadLine { get; set; }

		[Required]
		public Guid CreatorId { get; set; }
		public User Creator;

		public Guid? AssignedUserId { get; set; }
		public User? AssignedUser;
	}

	public enum TaskStatut
	{
		open,
		inProgress,
		done,
	}
}

