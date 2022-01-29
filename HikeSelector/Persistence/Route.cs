using HikeSelector.Persistence;
using HikeSelector.Resources;

namespace HikeSelector
{
    public record Route
    {
        public int RouteId { get; set; }
        public string Name { get; set; } = string.Empty;
        public RouteLength RouteLength { get; set; }
        public TravelTimeToRoute TravelTimeToRoute { get; set; }
        public string Country { get; set; } = AppResources.Switzerland;
    }
}