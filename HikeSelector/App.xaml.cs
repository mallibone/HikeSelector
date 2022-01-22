using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HikeSelector
{
    public partial class App : Application
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
