using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using HikeSelector.Ressources;
using ReactiveUI;
using ReactiveUI.XamForms;
using SQLite;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.CommunityToolkit.Markup.LeftToRight;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace HikeSelector
{
    public class DashboardPage : ReactiveContentPage<DashboardViewModel>
    {
        public DashboardPage()
        {
            On<iOS>().SetUseSafeArea(true);
            Build();
            BindingContext = new DashboardViewModel();
        }
        
        void Build () => Content = new Grid
        {
            Children =
            {
                new Label()
                {
                    Text = AppResources.NoRoutes,
                    FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
                    HorizontalOptions = LayoutOptions.Center
                }.Row(0),
                new Button
                {
                    Text = AppResources.SuggestRoute,
                    FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Button)),
                    HorizontalOptions = LayoutOptions.Center,
                }.Bind(Button.IsVisibleProperty, nameof(ViewModel.HasRoutes)).Row(0),
            }
        }; 
    }

    public class DashboardViewModel : ReactiveObject
    {
        public DashboardViewModel()
        {
            ExecuteRouteSuggestion = ReactiveCommand.Create(RouteSuggestion);
        }

        public ICommand ExecuteRouteSuggestion { get; }
        public bool HasRoutes { get; set; }

        private IEnumerable<Route> RouteSuggestion()
        {
            throw new System.NotImplementedException();
        }
    }

    public record Route
    {
        public string Name { get; init; } = default!;
        public string Description { get; init;  } = default!;
    }

    public class RouteEntity
    {
        [PrimaryKey, AutoIncrement]
        public int RouteId { get; set; }

        public string RouteName { get; set; } = string.Empty;
        public RouteLength RouteLength { get; set; }
        public TravelTimeToRoute TravelTimeToRoute { get; set; }
        public string Country { get; set; } = AppResources.Switzerland;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ChangedAt { get; set; }
    }

    public enum TravelTimeToRoute
    {
        None,
        Short,
        DayTrip,
        Overnight
    }

    public enum RouteLength
    {
        Short,
        Medium,
        Long
    }
}