using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using AutoMapper;
using HikeSelector.Persistence;
using ReactiveUI;
using Splat;

namespace HikeSelector.ViewModels
{
    public class RouteViewModel : ViewModelBase
    {
        public RouteViewModel(int routeId = 0, Routes? routes = null, IMapper? mapper = null)
        {
            var routes1 = routes ?? Locator.Current.GetService<Routes>() ?? throw new ArgumentException(nameof(routes));
            var mapper1 = mapper ?? Locator.Current.GetService<IMapper>() ?? throw new ArgumentException(nameof(mapper));
            
            this.WhenActivated(disposable =>
            {
                if (routeId > 0)
                {
                    Observable
                        .StartAsync(() => routes1.Get(routeId))
                        .Subscribe(route => mapper1.Map(route, this))
                        .DisposeWith(disposable);
                }
            });
        }
        public int RouteId { get; set; }
        public string Name { get; set; } = string.Empty;
        public RouteLength RouteLength { get; set; }
        public TravelTimeToRoute TravelTimeToRoute { get; set; }
        public string Country { get; set; } = String.Empty;
    }
}