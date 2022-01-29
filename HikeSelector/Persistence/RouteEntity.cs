using System;
using HikeSelector.Resources;
using SQLite;

namespace HikeSelector.Persistence
{
    public class RouteEntity
    {
        [PrimaryKey, AutoIncrement]
        public int RouteId { get; set; }

        public string Name { get; set; } = string.Empty;
        public RouteLength RouteLength { get; set; }
        public TravelTimeToRoute TravelTimeToRoute { get; set; }
        public string Country { get; set; } = AppResources.Switzerland;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ChangedAt { get; set; }
    }
}