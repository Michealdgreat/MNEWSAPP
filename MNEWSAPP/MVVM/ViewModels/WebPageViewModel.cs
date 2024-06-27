using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MNEWSAPP.MVVM.ViewModels
{
    public class WebPageViewModel : BindableObject
    {
        public ICommand BackCommand { get; }

        public WebPageViewModel()
        {
            BackCommand = new Command(OnBackButtonPressed);
        }

        private async void OnBackButtonPressed()
        {
            await Shell.Current.GoToAsync("//IndexPage");
        }
    }
}
