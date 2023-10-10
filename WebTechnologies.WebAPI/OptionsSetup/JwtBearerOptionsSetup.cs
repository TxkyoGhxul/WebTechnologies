using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebTechnologies.Application.Authentication;

namespace WebTechnologies.WebAPI.OptionsSetup;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptionsSetup;

    public JwtBearerOptionsSetup(JwtOptions jwtOptionsSetup)
    {
        _jwtOptionsSetup = jwtOptionsSetup;
    }

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtOptionsSetup.Issuer,
            ValidAudience = _jwtOptionsSetup.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptionsSetup.SecretKey))
        };
    }
}
