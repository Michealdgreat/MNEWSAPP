using MNEWSAPP.MVVM.Models;
using System.Collections.Generic;
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
                article = value;
                OnPropertyChanged();
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

        }



        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if (sender is Label label && label.BindingContext is ArticleModel article)
            {
                var navigationParameters = new Dictionary<string, object> { { "article", article } };
                await Shell.Current.GoToAsync($"{nameof(ArticleDetailsView)}", navigationParameters);

                //await Shell.Current.GoToAsync($"ArticleDetailsView", navigationParameters);
            }
        }
    }
}