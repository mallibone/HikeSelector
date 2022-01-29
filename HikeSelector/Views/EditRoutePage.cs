using HikeSelector.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Entry = Xamarin.Forms.Entry;
using Picker = Xamarin.Forms.Picker;

namespace HikeSelector.Views
{
    public class EditRoutePage : ReactiveContentPage<RouteViewModel>
    {
        public EditRoutePage(int routeId = 0)
        {
            On<iOS>().SetUseSafeArea(true);
            BindingContext = new RouteViewModel(routeId);
            BackgroundColor = Color.FromHex("#DBEEB4");
            Build();
            this.WhenActivated(_ => { });
        }

        private void Build() =>
            Content = new Grid()
            {
                Children =
                {
                    new Frame()
                    {
                        Margin = new Thickness(36),
                        Padding = new Thickness(10),
                        BackgroundColor = Color.White,
                        Content = new StackLayout()
                        {
                            Children =
                            {
                                new Label()
                                {
                                    Text = HikeSelector.Resources.AppResources.RouteEdit,
                                    FontSize = 24,
                                    FontAttributes = FontAttributes.Bold,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    VerticalTextAlignment = TextAlignment.Center,
                                    Margin = new Thickness(0, 0, 0, 10)
                                },
                                new Label()
                                {
                                    Text = HikeSelector.Resources.AppResources.Name,
                                    FontSize = 16,
                                    FontAttributes = FontAttributes.Bold,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    VerticalTextAlignment = TextAlignment.Center,
                                    Margin = new Thickness(0, 0, 0, 10)
                                },
                                new Entry()
                                {
                                    Placeholder = HikeSelector.Resources.AppResources.Name,
                                    Margin = new Thickness(0, 0, 0, 10)
                                }.Bind(Entry.TextProperty, nameof(ViewModel.Name)),
                                // new Label()
                                // {
                                //     Text = "Description",
                                //     FontSize = 16,
                                //     FontAttributes = FontAttributes.Bold,
                                //     HorizontalTextAlignment = TextAlignment.Center,
                                //     VerticalTextAlignment = TextAlignment.Center,
                                //     Margin = new Thickness(0, 0, 0, 10)
                                // },
                                // new Entry()
                                // {
                                //     Placeholder = "Description",
                                //     Margin = new Thickness(0, 0, 0, 10)
                                // }.Bind(Entry.TextProperty, nameof(ViewModel.)),
                                new Label()
                                {
                                    Text = HikeSelector.Resources.AppResources.RouteLength,
                                    FontSize = 16,
                                    FontAttributes = FontAttributes.Bold,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    VerticalTextAlignment = TextAlignment.Center,
                                    Margin = new Thickness(0, 0, 0, 10)
                                },
                                new Picker()
                                {
                                    Items =
                                    {
                                        HikeSelector.Resources.AppResources.RouteLenghtShort,
                                        HikeSelector.Resources.AppResources.RouteLenghtMedium,
                                        HikeSelector.Resources.AppResources.RouteLengthLong
                                    },
                                    Margin = new Thickness(0, 0, 0, 10)
                                }.Bind(Picker.SelectedIndexProperty, nameof(ViewModel.RouteLength)),
                                new Label()
                                {
                                    Text = HikeSelector.Resources.AppResources.TimeToTravel,
                                    FontSize = 16,
                                    FontAttributes = FontAttributes.Bold,
                                    HorizontalTextAlignment = TextAlignment.Center
                                },
                                new Picker()
                                {
                                    Items =
                                    {
                                        HikeSelector.Resources.AppResources.TTRNone,
                                        HikeSelector.Resources.AppResources.TTRShort,
                                        HikeSelector.Resources.AppResources.TTRDayTrip,
                                        HikeSelector.Resources.AppResources.TTROverNight
                                    },
                                    Margin = new Thickness(0, 0, 0, 10)
                                }.Bind(Picker.SelectedIndexProperty, nameof(ViewModel.TravelTimeToRoute)),
                            }
                        }
                    }
                }
            };
    }
}