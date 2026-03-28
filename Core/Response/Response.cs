using Core.Enums;
using Core.Extensions;

namespace Core.Response;

public class Response<T>
{
    public T? Data { get; set; }
    public string Status { get; set; }
    public bool IsSuccess { get; set; }

    public static Response<T> Success(T data)
    {
        return new Response<T>()
        {
            Data = data,
            IsSuccess = true,
            Status = EStatues.ONSUCCESS.GetDescription()
        };
    }

    public static Response<T> Failure(EStatues status, string message)
    {
        return new Response<T>()
        {
            Data =default,
            IsSuccess = false,
            Status = status.GetDescription() + ": " + message
        };
    }
    
    
}