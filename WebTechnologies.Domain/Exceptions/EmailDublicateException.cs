namespace WebTechnologies.Domain.Exceptions;

public class EmailDublicateException : Exception
{
    public EmailDublicateException() : base("Email is already exists")
    {
    }

    public EmailDublicateException(string email) : base($"Email is already exists. Current value: {email}")
    {
        Email = email;
    }

    public string Email { get; init; }
}
