using MNEWSAPP.MVVM.Views;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;
using MNEWSAPP.MVVM.ViewModels;

namespace MNEWSAPP
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Resolve HomeView from the service provider
            //var homeView = serviceProvider.GetRequiredService<HomeView>();
            //MainPage = new NavigationPage(homeView);

            //MainPage = new NavigationPage(new IndexPage());
            MainPage = new InterestView();

        }
    }
}
