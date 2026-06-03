using backend_api.Data;
using backend_api.DTOs;
using backend_api.Interfaces;
using backend_api.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _context;

    private readonly IJwtService _jwtService;

    public AuthRepository(
        ApplicationDbContext context,
        IJwtService jwtService)
    {
        _context = context;

        _jwtService = jwtService;
    }

    public async Task<User> Register(RegisterDto dto)
    {
        var user = new User
        {
            Name = dto.Name,

            Email = dto.Email,

            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<string> Login(LoginDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        bool validPassword =
            BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash);

        if (!validPassword)
        {
#pragma warning disable S112
            throw new Exception("Invalid password");
#pragma warning restore S112
        }

        return _jwtService.GenerateToken(user);
    }
}