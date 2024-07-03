using Microsoft.Maui.Controls;
using CustomShellMaui;
using MNEWSAPP.MVVM.Views;
using CustomShellMaui.Enum;

namespace MNEWSAPP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ArticleDetailsView), typeof(ArticleDetailsView));
            Routing.RegisterRoute(nameof(WebPageView), typeof(WebPageView));
            Routing.RegisterRoute(nameof(CategoryView), typeof(CategoryView));
            Routing.RegisterRoute(nameof(SearchView), typeof(SearchView));
            Routing.RegisterRoute(nameof(SetApiKeyView), typeof(SetApiKeyView));
            Routing.RegisterRoute(nameof(ChangeFontSizeView), typeof(ChangeFontSizeView));
            Routing.RegisterRoute(nameof(ConnectionErrorView), typeof(ConnectionErrorView));


            this.CustomShellMaui(new CustomShellMaui.Models.Transitions
            {
                Root = new CustomShellMaui.Models.TransitionRoot
                {
                    CurrentPage = TransitionType.FadeOut,
                    NextPage = TransitionType.LeftIn
                },
                Push = new CustomShellMaui.Models.Transition
                {
                    CurrentPage = TransitionType.LeftOut,
                    NextPage = TransitionType.RightIn
                },
                Pop = new CustomShellMaui.Models.Transition
                {
                    CurrentPage = TransitionType.RightOut,
                    NextPage = TransitionType.LeftIn
                }
            });
        }

        private async void SearchBarTapped(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SearchView)}");
        }
    }
}