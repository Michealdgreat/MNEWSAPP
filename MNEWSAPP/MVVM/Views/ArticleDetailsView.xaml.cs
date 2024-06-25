using MNEWSAPP.MVVM.Models;

namespace MNEWSAPP.MVVM.Views;

public partial class ArticleDetailsView : ContentPage
{
    public ArticleDetailsView(ArticleModel articleModel)
    {
        InitializeComponent();
        BindingContext = articleModel;
    }
}