using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Entities
{
	public class User
	{
		[Key]
		public Guid Id { get; set; }

        [Index(IsUnique = true)]
        public string Email { get; set; }

        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
		public bool IsManager { get; set; } 
    }
}