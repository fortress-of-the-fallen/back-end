namespace Presentation.Models.Responses.Base;

public class ExecutionRes
{
    public string? ErrorCode { get; set; } = string.Empty;

    public string? Error { get; set; } = string.Empty;

    public ValidateRes[]? Validates { get; set; } = Array.Empty<ValidateRes>();

    public bool Success => string.IsNullOrEmpty(ErrorCode) && Validates?.Length == 0;
}