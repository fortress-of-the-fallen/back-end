namespace Domain.Exceptions;

public class DeferredException : Exception
{
    public DeferredException(string message) : base(message) {}
}