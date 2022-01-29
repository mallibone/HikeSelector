using HikeSelector.Views;
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

            MainPage = new NavigationPage(new DashboardPage());
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
