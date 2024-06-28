using MNEWSAPP.MVVM.Models;

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
        }
    }

    public ArticleDetailsView()
    {
        InitializeComponent();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        BindingContext = _article;

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        if (_article != null)
        {
            string? url = _article.Url;

            if (!string.IsNullOrEmpty(url))
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { "url", url }
                };
                await Shell.Current.GoToAsync($"{nameof(WebPageView)}", navigationParameter);
            }
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {

        var currentStack = Shell.Current.Navigation.NavigationStack;
        System.Diagnostics.Debug.WriteLine($"Current Navigation Stack: {string.Join(", ", currentStack.Select(p => p.GetType().Name))}");

        //await Shell.Current.GoToAsync("..");

        await Shell.Current.GoToAsync("..");
    }

}