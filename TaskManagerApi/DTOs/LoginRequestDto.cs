using System;
using System.Text.RegularExpressions;

namespace TaskManager.DTOs
{
	public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool isValide(out string message)
        {
            if (!IsValidEmail(Email))
            {
                message = "Email format is invalid.";
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                message = "Password is required.";
                return false;
            }
            message = "";
            return true;
        }

        private bool IsValidEmail(string email)
        {
            // Regular expression pattern for validating email address format
            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            // Check if the email matches the pattern
            return Regex.IsMatch(email, pattern);
        }
    }
}

