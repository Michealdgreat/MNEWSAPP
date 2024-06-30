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

            //Routing.RegisterRoute(nameof(IndexPage), typeof(IndexPage));
            Routing.RegisterRoute(nameof(ArticleDetailsView), typeof(ArticleDetailsView));
            Routing.RegisterRoute(nameof(WebPageView), typeof(WebPageView));
            Routing.RegisterRoute(nameof(CategoryView), typeof(CategoryView));
            Routing.RegisterRoute(nameof(SearchView), typeof(SearchView));


            this.CustomShellMaui(new CustomShellMaui.Models.Transitions
            {
                Root = new CustomShellMaui.Models.TransitionRoot
                {
                    CurrentPage = TransitionType.FadeOut,
                    NextPage = TransitionType.LeftIn
                },
                Push = new CustomShellMaui.Models.Transition
                {
                    CurrentPage = TransitionType.TopOut,
                    NextPage = TransitionType.BottomIn
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