using MNEWSAPP.MVVM.ViewModels;

namespace MNEWSAPP.MVVM.Views;

public partial class IndexPage : ContentPage
{
    private HomeViewModel homeViewModel;

    public IndexPage()
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