using TechnicalAxos_RaulPimienta.ViewModels;

namespace TechnicalAxos_RaulPimienta.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel();
        }
    }
}
