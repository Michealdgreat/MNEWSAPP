using MNEWSAPP.MVVM.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MNEWSAPP.MVVM.Views
{
    [QueryProperty(nameof(ArticleModel), "data")]
    public partial class CategoryView : ContentPage, INotifyPropertyChanged
    {
        private List<ArticleModel>? article;
        private List<ArticleModel>? takeone;

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

        public List<ArticleModel>? TakeOne
        {
            get => takeone;
            set
            {
                if (takeone != value)
                {
                    takeone = value;
                    OnPropertyChanged();
                }
            }
        }

        public CategoryView()
        {
            InitializeComponent();
            takeone = [];
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetOne();
        }

        private void GetOne()
        {
            TakeOne?.Clear();
            if (ArticleModel != null)
            {
                foreach (var item in ArticleModel.Take(1))
                {
                    TakeOne?.Add(item);
                }
            }
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
