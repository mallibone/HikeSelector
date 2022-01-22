using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using HikeSelector.Persistence;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace HikeSelector
{
    public abstract class ViewModelBase: ReactiveObject, IActivatableViewModel
    {
        public ViewModelBase()
        {
            Activator = new ViewModelActivator();
        }

        public ViewModelActivator Activator { get; }
    }
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly Routes _routes;

        public DashboardViewModel(Routes? routes = null, IMapper? mapper = null)
        {
            ExecuteRouteSuggestion = ReactiveCommand.CreateFromObservable(RouteSuggestion);
            _routes = routes ?? Locator.Current.GetService<Routes>() ?? throw new ArgumentNullException();
            _mapper = mapper ?? Locator.Current.GetService<IMapper>() ?? throw new ArgumentNullException();

            this.WhenAnyValue( vm => vm.SuggestedRoute)
                .Select(sr => sr.Name)
                .ToPropertyEx(this, vm => vm.SuggestedRouteName);
            
            this.WhenActivated(disposable =>
            {
                Observable.StartAsync(() => _routes.Get())
                    .Subscribe(r => HasRoutes = r.Any())
                    .DisposeWith(disposable);
            });
        }

        public ICommand ExecuteRouteSuggestion { get; }
        [Reactive] public RouteViewModel SuggestedRoute { get; set; } = new() {Name = "Gnabber"};
        public string SuggestedRouteName { [ObservableAsProperty] get; } = string.Empty;
        [Reactive] public bool HasRoutes { get; set; }

        private IObservable<IEnumerable<RouteViewModel>> RouteSuggestion() =>
            Observable.StartAsync(_routes.Get)
                .Select(r => r.Select(_mapper.Map<RouteViewModel>).OrderBy(_ => Guid.NewGuid()))
                .Do(r => SuggestedRoute = r.First());
    }

    public class RouteViewModel : ViewModelBase
    {
        public int RouteId { get; set; }
        public string Name { get; set; } = string.Empty;
        public RouteLength RouteLength { get; set; }
        public TravelTimeToRoute TravelTimeToRoute { get; set; }
        public string Country { get; set; } = String.Empty;
    }
}