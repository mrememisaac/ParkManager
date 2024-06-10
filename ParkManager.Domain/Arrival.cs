using System.Collections.ObjectModel;

namespace ParkManager.Domain
{
    public class Arrival : ArrivalDepartureBase
    {
        public ReadOnlyCollection<ArrivalImage> Images => _images.AsReadOnly();

        public List<ArrivalImage> _images = new List<ArrivalImage>();
    }
}
