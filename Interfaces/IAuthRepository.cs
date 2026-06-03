using backend_api.DTOs;
using backend_api.Models;

namespace backend_api.Interfaces;

public interface IAuthRepository
{
       Task<User> Register(RegisterDto dto);

       Task<string> Login(LoginDto dto);
}
