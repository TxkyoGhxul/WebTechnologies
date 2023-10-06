using WebTechnologies.Domain.Models;

namespace WebTechnologies.Domain.Constants;
public static class Roles
{
    public static Role User => new(nameof(User));
    public static Role Admin => new(nameof(Admin));
    public static Role Support => new(nameof(Support));
    public static Role SuperAdmin => new(nameof(SuperAdmin));
}
