namespace ParkManager.Domain
{
    public class Slot : Entity
    {
        public int LaneId { get; private set; }

        public string Name { get; set; }

        public virtual Lane Lane { get; set; }
    }
}
