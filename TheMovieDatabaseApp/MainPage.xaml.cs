using TheMovieDatabaseApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheMovieDatabaseApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(Navigation);
        }
    }
}