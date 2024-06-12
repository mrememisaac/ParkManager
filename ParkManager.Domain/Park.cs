﻿using System.Collections.ObjectModel;

namespace ParkManager.Domain
{
    /// <summary>
    /// Represents a park entity.
    /// </summary>
    public class Park : Entity
    {
        /// <summary>
        /// Gets or sets the name of the park.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the street of the park.
        /// </summary>
        public string Street { get; private set; }

        /// <summary>
        /// Gets or sets the city of the park.
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// Gets or sets the state of the park.
        /// </summary>
        public string State { get; private set; }

        /// <summary>
        /// Gets or sets the country of the park.
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Gets or sets the latitude of the park.
        /// </summary>
        public long Latitude { get; private set; }

        /// <summary>
        /// Gets or sets the longitude of the park.
        /// </summary>
        public long Longitude { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Park"/> class.
        /// </summary>
        /// <param name="name">The name of the park.</param>
        /// <param name="street">The street of the park.</param>
        /// <param name="city">The city of the park.</param>
        /// <param name="state">The state of the park.</param>
        /// <param name="country">The country of the park.</param>
        /// <param name="latitude">The latitude of the park.</param>
        /// <param name="longitude">The longitude of the park.</param>
        public Park(string name, string street, string city, string state, string country, long latitude, long longitude)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Street = street ?? throw new ArgumentNullException(nameof(street));
            City = city ?? throw new ArgumentNullException(nameof(city));
            State = state ?? throw new ArgumentNullException(nameof(state));
            Country = country ?? throw new ArgumentNullException(nameof(country));
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Gets a read-only collection of park images.
        /// </summary>
        public ReadOnlyCollection<ParkImage> Images => _images.AsReadOnly();

        /// <summary>
        /// A list of park images.
        /// This field is used internally to manage the collection of images.
        /// </summary>
        public List<ParkImage> _images = new List<ParkImage>();

        /// <summary>
        /// Adds an image to the park.
        /// </summary>
        /// <param name="image">The image to add.</param>
        public void AddImage(ParkImage image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            _images.Add(image);
        }

        /// <summary>
        /// Removes an image from the park.
        /// </summary>
        /// <param name="image">The image to remove.</param>
        public void RemoveImage(ParkImage image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            _images.Remove(image);
        }

        /// <summary>
        /// Gets a read-only collection of park lanes.
        /// </summary>
        public ReadOnlyCollection<Lane> Lanes => _lanes.AsReadOnly();

        /// <summary>
        /// A list of park lanes.
        /// This field is used internally to manage the collection of lanes.
        /// </summary>
        public List<Lane> _lanes = new List<Lane>();

        /// <summary>
        /// Adds a lane to the park.
        /// </summary>
        /// <param name="lane">The lane to add.</param>
        public void AddLane(Lane lane)
        {
            if (lane == null)
            {
                throw new ArgumentNullException(nameof(lane));
            }

            if (_lanes.Contains(lane))
            {
                throw new ArgumentException("The lane has already been added.", nameof(lane));
            }

            _lanes.Add(lane);
        }

        /// <summary>
        /// Removes a lane from the park.
        /// </summary>
        /// <param name="lane">The lane to remove.</param>
        public void RemoveLane(Lane lane)
        {
            if (lane == null)
            {
                throw new ArgumentNullException(nameof(lane));
            }

            _lanes.Remove(lane);
        }

    }
}
