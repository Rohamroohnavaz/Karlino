namespace WebLayer.Models
{
    public class BaseResponseDto<T> : ResponseDto
    {
        public T? Data { get; set; }

        public BaseResponseDto() { }

        public BaseResponseDto(T data)
        {
            Data = data;
            IsSuccess = true;
        }

        public BaseResponseDto(string code, string message)
        {
            IsSuccess = false;
            BaseError = new BaseError
            {
                Code = code,
                Message = message
            };
        }

        public static BaseResponseDto<T> Success(T data)
        {
            return new BaseResponseDto<T>(data);
        }

        public static new BaseResponseDto<T> Failure(string code, string message)
        {
            return new BaseResponseDto<T>(code, message);
        }
    }
}
