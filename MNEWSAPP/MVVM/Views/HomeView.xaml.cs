using Microsoft.Extensions.Configuration;
using MNEWSAPP.MVVM.ViewModels;

namespace MNEWSAPP.MVVM.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView(HomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            // Automatically trigger the load command when the view is initialized
            //if (BindingContext is HomeViewModel viewModel)
            //{
            //    viewModel.LoadCommand.Execute(null);
            //}
        }
    }
}
