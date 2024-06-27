using MNEWSAPP.MVVM.Views;

namespace MNEWSAPP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("IndexPage", typeof(IndexPage));
            Routing.RegisterRoute("ArticleDetailsView", typeof(ArticleDetailsView));
            Routing.RegisterRoute("WebPageView", typeof(WebPageView));
            Routing.RegisterRoute("CategoryView", typeof(CategoryView));
        }
    }
}