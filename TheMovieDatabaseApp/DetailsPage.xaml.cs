using TheMovieDatabaseApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheMovieDatabaseApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage()
        {
            InitializeComponent();
        }

        public DetailsPage(Movie movie) : this()
        {
            BindingContext = movie;
        }
    }
}