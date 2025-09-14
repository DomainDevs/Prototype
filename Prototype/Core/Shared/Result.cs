namespace Shared;

public class Result<T>
{
    public bool IsSuccess { get; }      // Indica si la operación fue exitosa
    public T? Value { get; }            // El valor si la operación fue exitosa
    public string? ErrorMessage { get; } // El mensaje de error si la operación falló

    private Result(bool isSuccess, T? value, string? errorMessage)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Success(T value) => new Result<T>(true, value, null);
    public static Result<T> Failure(string errorMessage) => new Result<T>(false, default(T), errorMessage);
}