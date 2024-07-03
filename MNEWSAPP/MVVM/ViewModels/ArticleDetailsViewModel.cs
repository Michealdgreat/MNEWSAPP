using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MNEWSAPP.MVVM.Messages;
using MNEWSAPP.MVVM.Models;

namespace MNEWSAPP.MVVM.ViewModels
{
    public partial class ArticleDetailsViewModel : ObservableObject
    {
        private ArticleModel? _article;
        public ArticleModel? Article
        {
            get => _article;
            set => SetProperty(ref _article, value);
        }

        private int _fontSize;
        public int FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }

        public ArticleDetailsViewModel()
        {
            WeakReferenceMessenger.Default.Register<FontSizeChangedMessage>(this, (r, m) =>
            {
                FontSize = m.Value;
            });
        }
    }
}