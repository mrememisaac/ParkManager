using System.Collections.ObjectModel;

namespace ParkManager.Domain
{
    public class Departure : ArrivalDepartureBase
    {
        public ReadOnlyCollection<DepartureImage> Images => _images.AsReadOnly();

        public List<DepartureImage> _images = new List<DepartureImage>();
    }
}
