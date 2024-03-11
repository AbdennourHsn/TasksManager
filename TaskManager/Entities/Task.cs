using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Entities
{
	public class TaskEntity
	{
		[Key]
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Discription { get; set; }
		public TaskStatut Statut { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime DeadLine { get; set; }

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

