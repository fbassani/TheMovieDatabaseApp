using System;
using System.Threading.Tasks;
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
            Xamarin.Forms.Mocks.MockForms.Init();
            App.IsNetworkAvailabe = () => true;
            _navigationMock = new Mock<INavigation>();
            _movieDataSourceMock = new Mock<IMovieDataSource>();
            _movieDataSourceMock.Setup(m => m.GetMoviesAsync(It.IsAny<int>())).ReturnsAsync(new MoviesPage
            {
                TotalPages = 2
            });

            _viewModel = new MainPageViewModel(_navigationMock.Object, _movieDataSourceMock.Object);
            EnsureResources();
        }

        private static void EnsureResources()
        {
            new App();
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

        [Test]
        public void MovieSelectedCommand_WhenExecuted_ShouldPushDetailsPage()
        {
            _viewModel.MovieSelectedCommand.Execute(new Movie());
            _navigationMock.Verify(n => n.PushAsync(It.IsAny<DetailsPage>()));
        }

        [Test]
        public async Task OnLoadMore_ShouldGetMovies()
        {
            _movieDataSourceMock.Setup(d => d.GetMoviesAsync(It.IsAny<int>())).ReturnsAsync(new MoviesPage());
            await _viewModel.OnLoadMore();
            _movieDataSourceMock.Verify(m => m.GetMoviesAsync(1));
        }

        public void OnBeforeLoadMore_ShouldSetIsLoadigMoreToTrue()
        {
            _viewModel.OnBeforeLoadMore();
            Assert.IsTrue(_viewModel.IsLoadingMore);
        }

        public void OnAfterLoadMore_ShouldSetIsLoadingMoreToFalse()
        {
            _viewModel.OnAfterLoadMore();
            Assert.IsFalse(_viewModel.IsLoadingMore);
        }

        public void OnAfterLoadMore_ShouldSetHasErrorToFalse()
        {
            _viewModel.OnAfterLoadMore();
            Assert.IsFalse(_viewModel.HasError);
        }

        [Test]
        public async Task OnLoadMore_OnSecondCall_ShouldRequestPageTwo()
        {
            await CallOnLoadMoreTwice();
            _movieDataSourceMock.Verify(m => m.GetMoviesAsync(2));
        }

        private async Task CallOnLoadMoreTwice()
        {
            await _viewModel.OnLoadMore();
            await _viewModel.OnLoadMore();
        }

        [Test]
        public void OnError_ShouldSetHasErrorToTrue()
        {
            OnError();
            Assert.IsTrue(_viewModel.HasError);
        }

        [Test]
        public void OnError_ShouldSetIsLoadingMoreToFalse()
        {
            OnError();
            Assert.IsFalse(_viewModel.IsLoadingMore);
        }

        [TestCase(true, ExpectedResult = true)]
        [TestCase(false, ExpectedResult = false)]
        public bool OnCanLoadMore_ShouldConsiderNetworkAvailability(bool isAvailable)
        {
            App.IsNetworkAvailabe = () => isAvailable;
            return _viewModel.OnCanLoadMore();
        }

        [Test]
        public async Task CanLoadMore_WithAllPagesRequested_ShouldReturnFalse()
        {
            await CallOnLoadMoreTwice();
            var canLoadMore = _viewModel.OnCanLoadMore();
            Assert.IsFalse(canLoadMore);
        }

        private void OnError()
        {
            _viewModel.OnError(new Exception());
        }
    }
}