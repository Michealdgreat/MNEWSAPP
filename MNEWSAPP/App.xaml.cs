using MNEWSAPP.MVVM.Views;

using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace MNEWSAPP
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            Services = serviceProvider;

            MainPage = new HomeView();
        }
    }
}
