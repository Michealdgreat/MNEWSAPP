using MNEWSAPP.MVVM.Views;

namespace MNEWSAPP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new HomeView();
        }
    }
}
