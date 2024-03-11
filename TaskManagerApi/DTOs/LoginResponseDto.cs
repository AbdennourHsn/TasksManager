using System;
using TaskManager.Entities;

namespace TaskManager.DTOs
{
	public class LoginResponseDto
	{
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}

