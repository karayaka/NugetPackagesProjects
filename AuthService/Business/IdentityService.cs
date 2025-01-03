using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Interfaces;
using AuthService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Bussenes;

public class IdentityService(IHttpContextAccessor httpContextAccessor, IConfiguration config)
    : IIdentityService
{
    private readonly IConfiguration config = config;

    public SessionModel GetSesion()
    {
        var user = new SessionModel();

        if (httpContextAccessor.HttpContext == null)
        {
            user.ID = Guid.Empty;
            return user;
        }
        else if (httpContextAccessor.HttpContext.User == null)
        {
            user.ID = Guid.Empty;
            return user;
        }
        var val = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (val is not null) user.ID = new Guid(val.Value);
        var name = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name);
        if (name != null)
        {
            user.Name = name.Value;
        }
        var surname = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Surname);
        if (surname != null)
            user.Surname = surname.Value;
        var email = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email);
        if (email != null)
            user.Email = email.Value;
        var image = httpContextAccessor.HttpContext.User.FindFirst("image");
        if (image != null)
            user.Image = image.Value;
        return user;
    }
    public string JWTTokenGenerate(SessionModel user, DateTime expiredDate)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config["AppSettings:TokenKey"]);
            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
               {
                   new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                   new Claim(ClaimTypes.Name, user.Name),
                   new Claim(ClaimTypes.Surname, user.Surname),
                   new Claim(ClaimTypes.Email, user.Email),
                   new Claim("image", user.Image),
               }),
                Expires = expiredDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
               SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDecriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public SessionModel ResolveJwtToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadJwtToken(token);
        var claims = jsonToken.Claims.ToList();

        return new SessionModel
        {
            ID = new Guid(claims.FirstOrDefault(c => c.Type == "nameid")?.Value),
            Name = claims.FirstOrDefault(c => c.Type == "name")?.Value,
            Surname = claims.FirstOrDefault(c => c.Type == "family_name")?.Value,
            Email = claims.FirstOrDefault(c => c.Type == "email")?.Value,
            
            Token = token,
        };
    }
}