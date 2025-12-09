using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackBuildApi.Model
{

        public class ApiResponse<T>
        {
            public bool Succeeded { get; set; }
            public string Message { get; set; }
            public int StatusCode { get; set; }
            public List<string> Errors { get; set; }
            public T Data { get; set; }

            public ApiResponse(bool isSucceeded, string message, int statusCode, T data, List<string> errors)
            {
                Succeeded = isSucceeded;
                Message = message;
                StatusCode = statusCode;
                Data = data;
                Errors = errors;
            }
            public ApiResponse(bool isSucceeded, string message, int statusCode, T data)
            {
                Succeeded = isSucceeded;
                Message = message;
                StatusCode = statusCode;
                Data = data;
            }
            public ApiResponse(bool isSucceeded, string message, int statusCode, List<string> errors)
            {
                Succeeded = isSucceeded;
                Message = message;
                StatusCode = statusCode;
                Errors = errors;
            }


            public ApiResponse(bool v1, int status201Created, string v2)
            {
                Succeeded = v1;
                StatusCode = status201Created;
                Message = v2;
            }

            public static ApiResponse<T> Success(T data, string message, int statusCode)
            {
                return new ApiResponse<T>(true, message, statusCode, data, []);
            }
            public static ApiResponse<T> Failed(bool isSucceeded, string message, int statusCode, List<string> errors)
            {
                return new ApiResponse<T>(isSucceeded, message, statusCode, errors);
            }

            public static ApiResponse<T> Failed(string message, int statusCode, List<string> errors)
            {
                return new ApiResponse<T>(false, message, statusCode, errors);
            }

            public static ApiResponse<T> Failed(string message, int statusCode)
            {
                return new ApiResponse<T>(false, message, statusCode, new List<string>());
            }
        }

        public class ServiceResult<T>
        {
            public bool Success { get; set; }
            public T Data { get; set; }
            public string Message { get; set; }

            public static ServiceResult<T> SuccessResult(T data) => new() { Success = true, Data = data };
            public static ServiceResult<T> FailureResult(string message) => new() { Success = false, Message = message };
        }
    
}
