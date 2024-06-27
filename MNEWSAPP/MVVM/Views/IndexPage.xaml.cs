using CommunityToolkit.Maui.Core.Extensions;
using MNEWSAPP.MVVM.Models;
using MNEWSAPP.MVVM.ViewModels;
using MNEWSAPP.Service;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MNEWSAPP.MVVM.Views;

public partial class IndexPage : ContentPage
{
    private HomeViewModel homeViewModel;

    private GetNews _getnews = new();

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

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if (sender is Label label && label.BindingContext is ArticleModel article)
        {
            var navigationParameters = new Dictionary<string, object> { { "article", article } };
            await Shell.Current.GoToAsync($"{nameof(ArticleDetailsView)}", navigationParameters);

            //await Shell.Current.GoToAsync($"ArticleDetailsView", navigationParameters);
        }
    }

    private async void FraturedSectionTapped(object sender, TappedEventArgs e)
    {
        if (sender is Label label && label.BindingContext is ArticleModel article)
        {
            var navigationParameters = new Dictionary<string, object> { { "article", article } };
            await Shell.Current.GoToAsync($"{nameof(ArticleDetailsView)}", navigationParameters);

            //await Shell.Current.GoToAsync($"ArticleDetailsView", navigationParameters);
        }
    }

}