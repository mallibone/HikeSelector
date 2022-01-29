using HikeSelector.Views;

namespace HikeSelector
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            Bootstrapper.CreateMapper();
            Bootstrapper.RegisterServices();

            MainPage = new DashboardPage();
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
