using Shared.DTOs;

namespace Shared.Helpers
{
    public static class ApiResponse
    {
        public static ResponseDTO<T> Success<T>(T data, string? message = null)
            => new ResponseDTO<T> { Success = true, Message = message, Data = data };

        public static ResponseDTO<T> Fail<T>(string message, T? data = default)
            => new ResponseDTO<T> { Success = false, Message = message, Data = data };
    }
}
