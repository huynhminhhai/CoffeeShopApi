namespace CoffeeShopApi.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string ErrorCode { get; set; } = string.Empty;

        public ApiResponse() { }

        public ApiResponse(bool success, T? data, string message = "", int statusCode = 200, string errorCode = "")
        {
            Success = success;
            Data = data;
            Message = message;
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "", int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static ApiResponse<T> ErrorResponse(string message, int statusCode = 500, string errorCode = "")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = default,
                Message = message,
                StatusCode = statusCode,
                ErrorCode = errorCode
            };
        }
    }
}
