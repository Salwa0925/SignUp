using System;

namespace UserApi;

/// <summary>
/// Represents the result of a service(signUp and logIn) operation.
/// </summary>
public class ServiceResult<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public string? ErrorMessage {get;init;}

    public static ServiceResult<T> CreateResult(T data)
    {
        return new ServiceResult<T>
        {
            Success = true,
            Data = data
        };
    }
    public static ServiceResult<T> CreateFailure(string message)
    {
        return new ServiceResult<T>
        {
            Success = false,
            ErrorMessage = message
        };
    }

}