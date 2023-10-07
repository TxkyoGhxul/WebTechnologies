using System.ComponentModel.DataAnnotations.Schema;
using WebTechnologies.Domain.Models.Base;
using WebTechnologies.Domain.ValueObjects;

namespace WebTechnologies.Domain.Models;
public class User : Entity
{
    public DateOnly BirthDate { get; set; }
    public string Name { get; set; }
    public Email Email { get; set; }
    public List<Role> Roles { get; set; }

    private User() // EF needed
    {
    }

    public User(DateOnly birthDate, string name, Email email, List<Role> roles)
        : this(Guid.NewGuid(), birthDate, name, email, roles)
    {
    }

    public User(DateOnly birthDate, string name, Email email)
        : this(Guid.NewGuid(), birthDate, name, email, new())
    {
    }

    public User(Guid id, DateOnly birthDate, string name, Email email, List<Role> roles)
    {
        Id = id;
        BirthDate = birthDate;
        Name = name;
        Email = email;
        Roles = roles;
    }

    public int Age => WasBirthdayThisYear() ? 
        DateTime.UtcNow.Year - BirthDate.Year : 
        DateTime.UtcNow.Year - BirthDate.Year - 1;

    private bool WasBirthdayThisYear() => 
        DateTime.UtcNow.Month > BirthDate.Month || 
        (DateTime.UtcNow.Month == BirthDate.Month && DateTime.UtcNow.Day >= BirthDate.Day);
}
