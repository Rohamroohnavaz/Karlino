namespace WebLayer.Models
{
    public class GeneralResult
    {
        public GeneralResult(Guid id)
        {
            ResourceId = id;
        }
        public Guid ResourceId { get; set; }
    }
}
