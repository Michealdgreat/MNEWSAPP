using CommunityToolkit.Mvvm.ComponentModel;
using MNEWSAPP.MVVM.Models;
using MNEWSAPP.MVVM.ViewModels;

namespace MNEWSAPP.MVVM.Views;

[QueryProperty(nameof(Article), "article")]
public partial class ArticleDetailsView : ContentPage
{
    private ArticleModel? _article;

    public ArticleModel? Article
    {
        get => _article;
        set
        {
            _article = value;
            OnPropertyChanged();
            var viewModel = (ArticleDetailsViewModel)BindingContext;
            viewModel.Article = value;
        }
    }

    public ArticleDetailsView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (_article != null)
        {
            string? url = _article.Url;
            if (!string.IsNullOrEmpty(url))
            {
                var navigationParameter = new Dictionary<string, object> { { "url", url } };
                await Shell.Current.GoToAsync($"{nameof(WebPageView)}", navigationParameter);
            }
        }
    }
}
