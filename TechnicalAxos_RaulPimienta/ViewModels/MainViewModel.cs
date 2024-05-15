using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using TechnicalAxos_RaulPimienta.Models.Responses;
using TechnicalAxos_RaulPimienta.Services;

namespace TechnicalAxos_RaulPimienta.ViewModels
{
    public partial class MainViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {

        [ObservableProperty]
        private string bundleId;

        [ObservableProperty]
        private ImageSource image;

        [ObservableProperty]
        private List<CountryResponse> allCountries;

        [ObservableProperty]
        private ObservableRangeCollection<CountryResponse> countries;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private int pageSize;

        [ObservableProperty]
        private bool isLoading;

        public MainViewModel(bool isTest = false)
        {
            Countries = new ObservableRangeCollection<CountryResponse>();
            PageSize = 5;
            Image = "https://random.dog/af70ad75-24af-4518-bf03-fec4a997004c.jpg";
            if (!isTest)
            {
                BundleId = AppInfo.PackageName;
                GetData();
            }
        }

        public void GetData()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                await RestServiceCall<List<CountryResponse>>.Get("v3.1/all", CountriesDataLoaded, CountriesDataLoadFailedAsync).ConfigureAwait(false);
                IsBusy = false;
            });
        }

        [RelayCommand]
        public async Task SelectImage()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var photo = await MediaPicker.Default.PickPhotoAsync();

                if (photo != null)
                {
                    var stream = await photo.OpenReadAsync();
                    Image = ImageSource.FromStream(() => stream);
                }
            }
        }

        private void CountriesDataLoaded(List<CountryResponse> countryResponses)
        {
            if (countryResponses != null)
            {
                AllCountries = countryResponses.ToList();
                Countries.AddRange(countryResponses.Take(PageSize).ToList());
            }
        }

        private void CountriesDataLoadFailedAsync(Exception exception)
        {
            if (!string.IsNullOrWhiteSpace(exception?.Message))
            {
                Task.Run(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Error", exception?.Message, "OK");
                });
            }
        }

        [RelayCommand]
        public async Task LoadMoreData()
        {
            try
            {
                if (IsLoading || IsBusy) { return; }

                if (AllCountries?.Count > 0)
                {
                    IsLoading = true;
                    await Task.Delay(1000);
                    Countries.AddRange(AllCountries.Skip(Countries.Count()).Take(PageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
