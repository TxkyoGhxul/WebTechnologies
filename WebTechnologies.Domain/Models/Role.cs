namespace WebTechnologies.Domain.Models;
public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<User> Users { get; set; } = new();

    private Role() // EF needed
    {
    }

    public Role(string name)
    {
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
