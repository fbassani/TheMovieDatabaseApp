using System;
using Xamarin.Forms;

namespace TheMovieDatabaseApp
{
    public partial class App : Application
    {
        public static Func<bool> IsNetworkAvailabe = () => true;

        public App()
        {
            InitializeComponent();

            var navigationPage = new NavigationPage(new MainPage());
            MainPage = navigationPage;
        }
    }
}