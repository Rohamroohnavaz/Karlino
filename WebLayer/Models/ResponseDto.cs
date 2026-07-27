namespace WebLayer.Models
{
    public class ResponseDto : BaseResponseDto<GeneralResult>
    {
        public ResponseDto(Guid id) : base(new GeneralResult(id))
        {
            
        }

        public ResponseDto(string message ,string code) : base(message, code) 
        {
            
        }
    }
}
