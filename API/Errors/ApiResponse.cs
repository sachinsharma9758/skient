using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultErrorMessage(statusCode);
        }


        public int StatusCode { get; set; }
        public string Message { get; set; }

        
        private string GetDefaultErrorMessage(int statusCode)
        {
            return StatusCode switch {
                400=> "A bad request",
                401=> "Not authorized",
                404=> "Resource not found",
                500=> "Internal Server Error",
                _=> null,
            } ;  
        }
    }
}