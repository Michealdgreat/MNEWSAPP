using MNEWSAPP.MVVM.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MNEWSAPP.MVVM.Views
{
    [QueryProperty(nameof(ArticleModel), "data")]
    public partial class CategoryView : ContentPage, INotifyPropertyChanged
    {
        private List<ArticleModel>? article;

        public List<ArticleModel>? ArticleModel
        {
            get => article;
            set
            {
                if (article != value)
                {
                    article = value;
                    OnPropertyChanged();
                }
            }
        }

        public CategoryView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ArticleModel?.Clear(); // Clear the articles list on appearing
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (sender is Label label && label.BindingContext is ArticleModel article)
            {
                var navigationParameters = new Dictionary<string, object> { { "article", article } };
                await Shell.Current.GoToAsync($"{nameof(ArticleDetailsView)}", navigationParameters);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
