using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Interfaces;
public interface IJwtTokenProvider
{
    string GetJwtToken(User user);
}
