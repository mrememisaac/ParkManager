using System.Collections.ObjectModel;

namespace ParkManager.Domain
{
    public class Vehicle : Entity
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }

        public ReadOnlyCollection<VehicleImage> Images => _images.AsReadOnly();

        public List<VehicleImage> _images = new List<VehicleImage>();
    }
}
