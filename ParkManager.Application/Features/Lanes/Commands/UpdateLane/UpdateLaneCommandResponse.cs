namespace ParkManager.Application.Features.Lanes.Commands.UpdateLane
{
    public class UpdateLaneCommandResponse
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the ID of the park associated with the lane.
        /// </summary>
        public Guid ParkId { get; private set; }

        /// <summary>
        /// Gets or sets the name of the lane.
        /// </summary>
        public string Name { get; private set; }
    }
}
