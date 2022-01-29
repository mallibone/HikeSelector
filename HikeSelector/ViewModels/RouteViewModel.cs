using System;
using HikeSelector.Persistence;

namespace HikeSelector.ViewModels
{
    public class RouteViewModel : ViewModelBase
    {
        public int RouteId { get; set; }
        public string Name { get; set; } = string.Empty;
        public RouteLength RouteLength { get; set; }
        public TravelTimeToRoute TravelTimeToRoute { get; set; }
        public string Country { get; set; } = String.Empty;
    }
}