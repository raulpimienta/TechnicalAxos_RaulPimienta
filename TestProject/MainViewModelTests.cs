using FluentAssertions;
using TechnicalAxos_RaulPimienta.ViewModels;

namespace TestProject
{
    public class MainViewModelTests
    {
        [Fact]
        public async void GetDataTest()
        {
            var viewModel = new MainViewModel(true);
            viewModel.GetData();
            while(viewModel.IsBusy)
            {
                Console.WriteLine("IsBusy");
            }
            viewModel.Countries.Should().HaveCount(x => x > 0);
        }

        [Fact]
        public async void LoadMoreDataTest()
        {
            var viewModel = new MainViewModel(true);
            viewModel.GetData();
            while (viewModel.IsBusy)
            {
                Console.WriteLine("IsBusy");
            }

            await viewModel.LoadMoreData();

            viewModel.Countries.Should().HaveCount(x => x == viewModel.PageSize * 2);
        }
    }
}