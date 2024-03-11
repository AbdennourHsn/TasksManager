using System;
using TaskManager.Entities;

namespace TaskManager.IServices
{
	public interface IJwtTokenService
    {
        string CreateToken(User user);
    }
}

