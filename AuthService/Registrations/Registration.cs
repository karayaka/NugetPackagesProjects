using System.Text;
using AuthService.Bussenes;
using AuthService.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Registrations;

public static class Registration
{
    /// <summary>
    /// Secred token appsetting dosyasından alınmalı ve adı 
    /// ""AppSettings": {
    ///            "TokenKey": "NoteWithWepApiSecretTokenGenarator"
    ///          },"
    /// </summary>
    /// <param name="services"></param>
    /// <param name="secretToken">Secred token appsetting dosyasından alınmalı</param>
    public static void AddAuthenticationServices(this IServiceCollection services, string secretToken)
    {
        services.AddHttpContextAccessor();//http context accesor eklenecek ıoc container e

        services.AddSingleton<IIdentityService, IdentityService>();
        services.AddScoped<IEncryptionService, EncryptionService>();
        services.AddScoped<IGenerateService, GenerateService>();

     

        var key = Encoding.ASCII.GetBytes(secretToken);
        services.AddAuthentication(x => {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,

            };

        });
    }
}