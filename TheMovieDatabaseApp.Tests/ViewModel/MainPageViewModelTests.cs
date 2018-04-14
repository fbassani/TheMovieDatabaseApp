using Moq;
using NUnit.Framework;
using TheMovieDatabaseApp.Model;
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
            _navigationMock = new Mock<INavigation>();
            _movieDataSourceMock = new Mock<IMovieDataSource>();
            _viewModel = new MainPageViewModel(_navigationMock.Object, _movieDataSourceMock.Object);
        }

        [Test]
        public void ViewModel_OnLoad_ShouldGetMovies()
        {
            _movieDataSourceMock.Verify(m => m.GetMovies(1));
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