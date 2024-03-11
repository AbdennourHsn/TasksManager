using System;
using TaskManager.Entities;

namespace TaskManager.DTOs
{
	public class TaskDto
	{
        public string Title { get; set; }
        public string Discription { get; set; }
        public TaskStatut Statut { get; set; }
        public DateTime deadLine { get; set; }
    }
}

