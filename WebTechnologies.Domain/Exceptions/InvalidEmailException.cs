namespace WebTechnologies.Domain.Exceptions;
public class InvalidEmailException : Exception
{
    public InvalidEmailException() : base("Provide valid email")
    {
    }

    public InvalidEmailException(string email) : base($"Provide valid email. Current value: {email}")
    {
        Email = email;
    }

    public string Email { get; init; }
}
