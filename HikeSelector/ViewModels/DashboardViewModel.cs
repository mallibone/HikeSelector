using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using AutoMapper;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Sextant;
using Splat;

namespace HikeSelector.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IViewStackService? _viewStackService;
        private readonly IMapper _mapper;
        private readonly Routes _routes;

        public DashboardViewModel(Routes? routes = null, IMapper? mapper = null, IViewStackService? viewStackService = null)
        {
            _routes = routes ?? Locator.Current.GetService<Routes>() ?? throw new ArgumentNullException();
            _mapper = mapper ?? Locator.Current.GetService<IMapper>() ?? throw new ArgumentNullException();
            _viewStackService = viewStackService 
                                ?? Locator.Current.GetService<IViewStackService>() 
                                ?? throw new ArgumentNullException();
            
            ExecuteRouteSuggestion = ReactiveCommand.CreateFromObservable(RouteSuggestion);
            ExecuteAddHike = ReactiveCommand.CreateFromObservable(() => _viewStackService.PushPage<RouteViewModel>());

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

        public override string Id { get; } = nameof(DashboardViewModel);
        public ICommand ExecuteRouteSuggestion { get; }
        [Reactive] public RouteViewModel SuggestedRoute { get; set; } = new() {Name = "Gnabber"};
        public string SuggestedRouteName { [ObservableAsProperty] get; } = string.Empty;
        [Reactive] public bool HasRoutes { get; set; }
        public ICommand ExecuteAddHike { get; set; }

        private IObservable<IEnumerable<RouteViewModel>> RouteSuggestion() =>
            Observable.StartAsync(_routes.Get)
                .Select(r => r.Select(_mapper.Map<RouteViewModel>).OrderBy(_ => Guid.NewGuid()))
                .Do(r => SuggestedRoute = r.First());
    }
}