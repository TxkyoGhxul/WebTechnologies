using System.Text.RegularExpressions;
using ValueOf;
using WebTechnologies.Domain.Exceptions;

namespace WebTechnologies.Domain.ValueObjects;
public class Email : ValueOf<string, Email>
{
    private static readonly Regex _emailRegex = new(_emailPattern);

    private const int _maxLength = 50;
    private const string _emailPattern = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";

    protected override void Validate()
    {
        var match = _emailRegex.Match(Value);
        if (!match.Success || Value.Length > _maxLength)
        {
            throw new InvalidEmailException(Value);
        }
    }

    protected override bool TryValidate()
    {
        try
        {
            Validate();
            return true;
        }
        catch (InvalidEmailException)
        {
            return false;
        }
    }

    public static implicit operator string(Email email) => email.Value;

    public static explicit operator Email(string email) => From(email);
}
