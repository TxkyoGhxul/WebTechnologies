using WebTechnologies.Domain.Models.Base;

namespace WebTechnologies.Domain.Models;
public class Role : Entity
{
    public string Name { get; set; }
    public List<User> Users { get; set; } = new();

    public Role() // EF needed
    {
    }

    public Role(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj)
    {
        return obj is Role role && role.Name == role.Name;
    }
}
