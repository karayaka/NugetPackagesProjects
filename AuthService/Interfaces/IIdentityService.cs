using AuthService.Models;

namespace AuthService.Interfaces;

public interface IIdentityService
{
    SessionModel GetSesion();

    string JWTTokenGenerate(SessionModel user, DateTime expiredDate);

    SessionModel ResolveJwtToken(string token);
}