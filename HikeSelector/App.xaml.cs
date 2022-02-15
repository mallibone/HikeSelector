using System;
using HikeSelector.ViewModels;
using HikeSelector.Views;
using Sextant;
using Sextant.XamForms;
using Splat;
using Xamarin.Forms;

namespace HikeSelector
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            Bootstrapper.CreateMapper();
            Bootstrapper.RegisterServices();
            Bootstrapper.RegisterNavigation();
            
            Locator
                .Current
                .GetService<IParameterViewStackService>()!
                .PushPage<DashboardViewModel>()
                .Subscribe();

            MainPage = Locator.Current.GetNavigationView("NavigationView");

            // MainPage = new NavigationPage(new DashboardPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
