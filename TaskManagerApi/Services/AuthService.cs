using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Entities;
using TaskManager.IServices;

namespace TaskManager.Services
{
	public class AuthService : IAuthService
    {
        private readonly DataContext _db;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ResponseDto _response;

        public AuthService(DataContext db , IJwtTokenService jwtTokenService)
		{
            _db = db;
            _jwtTokenService = jwtTokenService;
            _response = new();
		}

        public async Task<ResponseDto> Login(LoginRequestDto loginDto)
        {
            var user = await _db.users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginDto.Email.ToLower());
            if (user == null)
                return ResponseDto.Unauthorized("Invalid email.");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i])
                    return ResponseDto.Unauthorized("Invalid password.");
            }

            var token = _jwtTokenService.CreateToken(user);
            LoginResponseDto loginResponseDto = new LoginResponseDto{ UserName = user.UserName, Token = token };
            _response.Result = loginResponseDto;
            return _response;
        }

        public async Task<ResponseDto> Register(RegistrationRequestDto registrationDto)
        {
            try { 
                using var hmac = new HMACSHA512();
                var user=new User {
                    Email = registrationDto.Email,
                    UserName=registrationDto.UserName,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registrationDto.Password)),
                    PasswordSalt = hmac.Key,
                    IsManager = registrationDto.IsManager
                };
                var token = _jwtTokenService.CreateToken(user);
                await _db.users.AddAsync(user);
                await _db.SaveChangesAsync();
                LoginResponseDto loginResponseDto = new LoginResponseDto() { UserName = user.UserName, Token = token };
                _response.Result = loginResponseDto;
                return _response ;
            }
            catch (Exception ex)
            {
                return ResponseDto.BadRequest($"Fail to register {ex}");
            }
        }
    }
}

