using System;
namespace TaskManager.DTOs
{
	public class ResponseDto
	{
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
        public int StatusCode { get; set; } = HttpStatusCode.OK;

        public static ResponseDto Unauthorized(string message)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = message,
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

        public static ResponseDto NotAllowed(string message)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = message,
                StatusCode = HttpStatusCode.Forbidden
            };
        }

        public static ResponseDto NotFounded(string message)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = message,
                StatusCode = HttpStatusCode.NotFound
            };
        }

        public static ResponseDto BadRequest(string message)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = message,
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }
}

