namespace WebLayer.Models
{
    public class General
    {
        public General(Guid id)
        {
            ResourceId = id;
        }
        public Guid ResourceId { get; set; }
    }
}
