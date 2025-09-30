
namespace Shared.Exceptions;

public class ControlValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ControlValidationException(string message, IDictionary<string, string[]> errors)
        : base(message)
    {
        Errors = errors;
    }
}