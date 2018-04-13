using Xamarin.Forms;

namespace TheMovieDatabaseApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navigationPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.Black,
                BarTextColor = Color.White
            };
            MainPage = navigationPage;
        }
    }
}