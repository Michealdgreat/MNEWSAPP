using MNEWSAPP.MVVM.Views;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;
using MNEWSAPP.MVVM.ViewModels;

namespace MNEWSAPP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
