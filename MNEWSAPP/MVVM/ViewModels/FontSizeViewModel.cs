using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MNEWSAPP.MVVM.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNEWSAPP.MVVM.ViewModels
{
    public partial class FontSizeViewModel : ObservableObject
    {
        private int _fontSize;
        public int FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }

        [RelayCommand]
        private async void SaveFontSize()
        {
            //sender
            WeakReferenceMessenger.Default.Send(new FontSizeChangedMessage(FontSize));

            await Shell.Current.GoToAsync("..");
        }
    }
}
