using MNEWSAPP.MVVM.Models;
using MNEWSAPP.MVVM.ViewModels;

namespace MNEWSAPP.MVVM.Views;

public partial class IndexPage : ContentPage
{
    private HomeViewModel homeViewModel;
    private int myProperty;

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

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Label label = (Label)sender;
        //string title = label.Text;
        var article = label?.BindingContext as ArticleModel;
        // Perform your desired action, e.g., navigate to another page, show an alert, etc.
        //DisplayAlert("Label Tapped", $"You tapped on: {title}", "OK");

        if (article != null)
        {
            await Navigation.PushAsync(new ArticleDetailsView(article));
        }
    }
}