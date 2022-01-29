using HikeSelector.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace HikeSelector.Views
{
    internal class RouteCardView : ReactiveContentView<RouteViewModel>
    {
        private Label TheLabel = 
            new Label { TextColor = Color.Black, Text = "Static Gnabber", HorizontalOptions = LayoutOptions.Center };
        public RouteCardView()
        {
            Build();
            this.WhenActivated(_ => { });
        }
        
        public static readonly BindableProperty RouteProperty =
            BindableProperty.Create(nameof(Route), typeof(RouteViewModel), typeof(RouteCardView), propertyChanged: OnRouteChanged);

        public RouteViewModel? Route
        {
            get => (RouteViewModel)GetValue(RouteProperty);
            set => SetValue(RouteProperty, value);
        }
        
        static void OnRouteChanged (BindableObject bindable, object oldRoute, object newRoute)
        {
            ((RouteCardView)bindable).BindingContext = (RouteViewModel)newRoute;
            ((RouteCardView)bindable).ApplyBindings();
        }
        
        private void Build() => Content = new Grid
        {
            Children =
            {
                new Frame()
                {
                    CornerRadius = 4f,
                    HasShadow = true,
                    BackgroundColor = Color.White,
                    Content = TheLabel
                            .Bind(Label.TextProperty, path: nameof(Route.Name), fallbackValue: "Gnabber")
                }
            }
        };
    }
}