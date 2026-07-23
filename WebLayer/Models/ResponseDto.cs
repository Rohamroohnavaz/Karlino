namespace WebLayer.Models
{
    public class ResponseDto : BaseResponseDto<General>
    {
        public ResponseDto(Guid id) : base(new General(id))
        {
            
        }

        public ResponseDto(string message ,string code) : base(message, code) 
        {
            
        }
    }
}
