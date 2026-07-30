namespace WebLayer.Models
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public BaseError? BaseError { get; set; }

        public ResponseDto() { }

        public static ResponseDto Success()
        {
            return new ResponseDto { IsSuccess = true };
        }

        public static ResponseDto Failure(string code, string message)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                BaseError = new BaseError { Code = code, Message = message }
            };
        }
    }
}
