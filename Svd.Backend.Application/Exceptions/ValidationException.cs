using FluentValidation.Results;

namespace Svd.Backend.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public List<string> ValidationErrors { get; }

    public ValidationException(ValidationResult validationResult)
    {
        ValidationErrors = new List<string>();

        foreach (var validationError in validationResult.Errors)
        {
            ValidationErrors.Add(validationError.ErrorMessage);
        }
    }
}
