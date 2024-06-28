using Microsoft.Maui.Controls;
using MNEWSAPP.MVVM.Views;

namespace MNEWSAPP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(IndexPage), typeof(IndexPage));
            Routing.RegisterRoute(nameof(ArticleDetailsView), typeof(ArticleDetailsView));
            Routing.RegisterRoute(nameof(WebPageView), typeof(WebPageView));
            Routing.RegisterRoute(nameof(CategoryView), typeof(CategoryView));
        }
    }
}