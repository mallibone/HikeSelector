using HikeSelector.Ressources;
using HikeSelector.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace HikeSelector.Views
{
    public class DashboardPage : ReactiveContentPage<DashboardViewModel>
    {
        public DashboardPage()
        {
            On<iOS>().SetUseSafeArea(true);
            BindingContext = new DashboardViewModel();
            BackgroundColor = Color.FromHex("#DBEEB4");
            BuildNavigationBar();
            Build();
            this.WhenActivated(_ => { });
        }

        private void BuildNavigationBar()
        {
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Add Hike",
                // Icon = "add.png",
                Command = ViewModel?.ExecuteAddHike
            });
            
        }

        private void Build() => Content = new Grid
        {
            BackgroundColor = Color.FromHex("#DBEEB4"),
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
                    Margin = new Thickness(32),
                    Children =
                    {
                        new Frame
                        {
                            CornerRadius = 4f,
                            HasShadow = true,
                            BackgroundColor = Color.White,
                            Content = new Label { TextColor = Color.Black, Text = "Static Gnabber", HorizontalOptions = LayoutOptions.Center }
                                    .Bind(Label.TextProperty, path: nameof(ViewModel.SuggestedRoute), convert: (RouteViewModel? vm) => vm?.Name, fallbackValue: "Gnabber")
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
}