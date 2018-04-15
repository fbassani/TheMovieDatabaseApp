using Moq;
using NUnit.Framework;
using TheMovieDatabaseApp.ViewModel;
using Xamarin.Forms;

namespace TheMovieDatabaseApp.Tests.ViewModel
{
    public class MainPageViewModelTests
    {
        private Mock<INavigation> _navigationMock;
        private Mock<IMovieDataSource> _movieDataSourceMock;
        private MainPageViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            App.IsNetworkAvailabe = () => true;
            _navigationMock = new Mock<INavigation>();
            _movieDataSourceMock = new Mock<IMovieDataSource>();
            _viewModel = new MainPageViewModel(_navigationMock.Object, _movieDataSourceMock.Object);
        }

        [Test]
        public void ViewModel_OnLoad_ShouldGetMovies()
        {
            _movieDataSourceMock.Verify(m => m.GetMoviesAsync(1));
        }

        [TestCase(true, ExpectedResult = false)]
        [TestCase(false, ExpectedResult = true)]
        public bool NetworkUnavailable_ShouldReturnIfNetworkUnavailable(bool isAvailable)
        {
            App.IsNetworkAvailabe = () => isAvailable;
            return _viewModel.NetworkUnavailable;
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = false)]
        public bool NetworkAvailable_ShouldReturnIfNetworkAvailable(bool isAvailable)
        {
            App.IsNetworkAvailabe = () => isAvailable;
            return _viewModel.NetworkAvailable;
        }

        //TODO: how to test this?
        //[Test]
        //public void MovieSelectedCommand_WhenExecuted_ShouldPushDetailsPage()
        //{
        //    _viewModel.MovieSelectedCommand.Execute(new Movie());
        //    _navigationMock.Verify(n => n.PushAsync(It.IsAny<DetailsPage>()));
        //}
    }
}