using System;
namespace TaskManager.DTOs
{
	public class RegistrationRequestDto
	{
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsManager { get; set; }
    }
}

