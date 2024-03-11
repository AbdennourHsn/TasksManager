using System;
using TaskManager.DTOs;

namespace TaskManager.IServices
{
	public interface IAuthService
	{
        Task<ResponseDto> Register(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}

