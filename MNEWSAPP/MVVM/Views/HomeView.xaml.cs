using MNEWSAPP.MVVM.ViewModels;

namespace MNEWSAPP.MVVM.Views
{
    public partial class HomeView : ContentPage
    {
        private readonly HomeViewModel _viewModel;

        public HomeView(HomeViewModel homeViewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = homeViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.GetNews();
        }
    }
}