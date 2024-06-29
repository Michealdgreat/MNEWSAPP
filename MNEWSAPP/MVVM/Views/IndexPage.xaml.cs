using CommunityToolkit.Maui.Core.Extensions;
using MNEWSAPP.MVVM.Models;
using MNEWSAPP.MVVM.ViewModels;
using MNEWSAPP.Service;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MNEWSAPP.MVVM.Views
{
    public partial class IndexPage : ContentPage
    {
        private HomeViewModel homeViewModel;
        private GetNews _getnews;

        public IndexPage()
        {
            _getnews = new GetNews();
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
            }
        }

        private async void FraturedSectionTapped(object sender, TappedEventArgs e)
        {
            if (sender is Label label && label.BindingContext is ArticleModel article)
            {
                var navigationParameters = new Dictionary<string, object> { { "article", article } };
                await Shell.Current.GoToAsync($"{nameof(ArticleDetailsView)}", navigationParameters);
            }
        }

        private async Task CategoryTapped(object sender, TappedEventArgs e, string category)
        {
            List<ArticleModel> articleModel = new();

            var data = await _getnews.GetNewsAsync(category);
            if (data != null)
            {
                foreach (var item in data)
                {
                    articleModel.Add(item);
                }
            }

            var navigationParameters = new Dictionary<string, object>
            {
                { "data", articleModel }
            };

            await Shell.Current.GoToAsync("CategoryView", navigationParameters);
        }

        private async void WindowsTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Microsoft");
        private async void AppleTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Apple");
        private async void GoogleTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Google");
        private async void FacebokTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Facebook");
        private async void xLogoTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "twitter");
        private async void AmazonTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Amazon");
        private async void GithubTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "developer");
    }
}
