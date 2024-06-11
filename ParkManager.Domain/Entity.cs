using System.Reflection.Metadata.Ecma335;

namespace ParkManager.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
