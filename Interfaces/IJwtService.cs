using backend_api.Models;

namespace backend_api.Interfaces;

public interface IJwtService
{ 
    string GenerateToken(User user);
}
