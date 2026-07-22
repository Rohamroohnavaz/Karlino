namespace WebLayer.Models
{
    public class BaseResponseDto<TData>
    {
        public BaseResponseDto(TData data)
        {
            Data = data;
            IsSuccesss = true;
        }

        public BaseResponseDto(string code ,string message)
        {
            IsSuccesss = false;
            BaseError = new BaseError
            {
                Code = code,
                Message = message
            };
        }

        public TData? Data { get; set; }

        public bool IsSuccesss { get; set; }

        public BaseError? BaseError { get; set; }
    }
}
