using MNEWSAPP.MVVM.ViewModels;

namespace MNEWSAPP.MVVM.Views
{
    public partial class HomeView : ContentPage
    {
        private HomeViewModel homeViewModel;

        public HomeView()
        {
            InitializeComponent();
            homeViewModel = new HomeViewModel();
            BindingContext = homeViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await homeViewModel.GetNews();
        }
    }
}
