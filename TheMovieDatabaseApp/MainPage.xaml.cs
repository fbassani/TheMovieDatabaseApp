using TheMovieDatabaseApp.ViewModel;
using Xamarin.Forms;

namespace TheMovieDatabaseApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(Navigation);
        }
    }
}