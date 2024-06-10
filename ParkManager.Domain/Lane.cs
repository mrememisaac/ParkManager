namespace ParkManager.Domain
{
    public class Lane : Entity
    {
        public int ParkId { get; private set; }

        public string Name { get; set; }

        public virtual Park Park { get; set; }
    }
}
