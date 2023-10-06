namespace WebTechnologies.Domain.Exceptions;

public class InvalidDublicateException : Exception
{
    public InvalidDublicateException() : base("Email is already exists")
    {
    }

    public InvalidDublicateException(string email) : base($"Email is already exists. Current value: {email}")
    {
        Email = email;
    }

    public string Email { get; init; }
}
