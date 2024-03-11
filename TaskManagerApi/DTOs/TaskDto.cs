using System;
using TaskManager.Entities;

namespace TaskManager.DTOs
{
	public class TaskDto
	{
        public string Title { get; set; }
        public string Discription { get; set; }
        public TaskStatut Statut { get; set; } = TaskStatut.open;
        public DateTime deadLine { get; set; }

        public bool isValide(out string message)
        {
            if (string.IsNullOrEmpty(Title))
            {
                message = "Title is required.";
                return false;
            }
            if (deadLine < DateTime.Now)
            {
                message = "DeadLine is invalide.";
                return false;
            }
            message = "";
            return true;
        }
    }
}

