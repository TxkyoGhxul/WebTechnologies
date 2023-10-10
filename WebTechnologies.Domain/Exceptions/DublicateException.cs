namespace WebTechnologies.Domain.Exceptions;
public class DublicateException : Exception
{
    public DublicateException(string message) : base(message)
    {
    }
}
