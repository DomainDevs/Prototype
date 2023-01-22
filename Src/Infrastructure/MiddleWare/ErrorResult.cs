namespace Prototype.Infrastructure.MiddleWare;

public class ErrorResult<T>
{
    public T Data { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }

    public static ErrorResult<T> Fail(string errorMessage)
    {
        return new ErrorResult<T> { Succeeded = false, Message = errorMessage };
    }

    public static ErrorResult<T> Success(T data)
    {
        return new ErrorResult<T> { Succeeded = true, Data = data };
    }
}
