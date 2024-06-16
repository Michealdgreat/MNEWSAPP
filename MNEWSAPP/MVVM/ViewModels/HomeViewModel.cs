using System.Collections.ObjectModel;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MNEWSAPP.MVVM.Models;
using MNEWSAPP.Service;
using System.Windows.Input;

namespace MNEWSAPP.MVVM.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly GetNews _getNews;

        public ObservableCollection<ArticleModel> News { get; set; }
        public ICommand LoadCommand { get; }

        public HomeViewModel(GetNews getNews)
        {
            _getNews = getNews;
            News = new ObservableCollection<ArticleModel>();
            LoadCommand = new RelayCommand(OnLoad);
        }

        private async void OnLoad()
        {
            await LoadNewsAsync();
        }

        private async Task LoadNewsAsync()
        {
            var articles = await _getNews.GetNewsAsync();

            if (articles != null)
            {
                News.Clear();
                foreach (var article in articles)
                {
                    News.Add(article);
                }
            }
        }
    }
}
