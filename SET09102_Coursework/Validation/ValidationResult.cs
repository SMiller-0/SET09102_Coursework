namespace SET09102_Coursework.Validation;

/// <summary>
/// Represents the result of a validation operation.
/// </summary>
/// <remarks>
/// This class provides a standardized way to return validation results,
/// including both the success/failure status and any associated error message.
/// It uses the factory pattern with Success() and Failure() methods for creation.
/// </remarks>
public class ValidationResult
{
    /// <summary>
    /// Gets a value indicating whether the validation was successful.
    /// </summary>
    /// <value>True if validation passed; otherwise, false.</value>
    public bool IsValid { get; }

    /// <summary>
    /// Gets the error message when validation fails.
    /// </summary>
    /// <value>
    /// The error message describing the validation failure.
    /// Empty string if validation was successful.
    /// </value>
    public string ErrorMessage { get; }

    /// <summary>
    /// Initializes a new instance of the ValidationResult class.
    /// </summary>
    /// <param name="isValid">Indicates if the validation passed.</param>
    /// <param name="errorMessage">The error message if validation failed.</param>
    /// <remarks>
    /// This constructor is private to enforce the use of Success() and Failure() factory methods.
    /// </remarks>
    private ValidationResult(bool isValid, string errorMessage = "")
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Creates a ValidationResult indicating successful validation.
    /// </summary>
    /// <returns>A ValidationResult with IsValid set to true and no error message.</returns>
    public static ValidationResult Success() => new(true);

    /// <summary>
    /// Creates a ValidationResult indicating failed validation with an error message.
    /// </summary>
    /// <param name="errorMessage">The message describing why validation failed.</param>
    /// <returns>A ValidationResult with IsValid set to false and the specified error message.</returns>
    public static ValidationResult Failure(string errorMessage) => new(false, errorMessage);
}
