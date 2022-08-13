namespace Svd.Backend.Application.Responses;

public class BaseResponse<T>
{
    public bool Success { get; }
    public T? Value { get; }
    public string? Message { get; }
    public List<string> ValidationErrors { get; set; }


    public BaseResponse(T value, string? message = null)
    {
        Success = true;
        Message = message;
        Value = value;
        ValidationErrors = new List<string>();
    }

    public BaseResponse(bool success, string message)
    {
        Success = success;
        Message = message;
        ValidationErrors = new List<string>();
    }
}
