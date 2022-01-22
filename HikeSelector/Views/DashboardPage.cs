using System.IO;
using HikeSelector.Persistence;
using HikeSelector.Ressources;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Markup;
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
            this.WhenActivated(_ => { });
        }

        void Build() => Content = new Grid
        {
            Children =
            {
                new Label()
                {
                    Text = AppResources.NoRoutes,
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    HorizontalOptions = LayoutOptions.Center,
                }.Bind(IsVisibleProperty, nameof(ViewModel.HasRoutes), convert: (bool? hr) => !hr).Row(0),
                new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new RouteCardView()
                        {
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Margin = new Thickness(0, 0, 0, 20),
                            BindingContext = ViewModel?.SuggestedRoute,
                        },
                        new Button
                            {
                                Text = AppResources.SuggestRoute,
                                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                                HorizontalOptions = LayoutOptions.Center,
                                TextColor = Color.Black
                            }
                            .Bind(Button.CommandProperty, nameof(ViewModel.ExecuteRouteSuggestion)),
                    }
                }.Bind(IsVisibleProperty, nameof(ViewModel.HasRoutes)).Row(0)
            }
        };
    }

    internal class RouteCardView : ReactiveContentView<RouteViewModel>
    {
        void Build() => Content = new Grid
        {
            Children =
            {
                new Label { TextColor = Color.Black, Text = "Static Gnabber", HorizontalOptions = LayoutOptions.Center }
                    .Bind(Label.TextProperty, path: nameof(ViewModel.Name),
                        convert: (RouteEntity? sr) => sr?.Name ?? ""),
            }
        };
    }
}