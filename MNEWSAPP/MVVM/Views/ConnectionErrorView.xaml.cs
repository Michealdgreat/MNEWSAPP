using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace MNEWSAPP.MVVM.Views
{
    [QueryProperty(nameof(Message), "errorMessage")]
    public partial class ConnectionErrorView : ContentPage, INotifyPropertyChanged
    {
        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        public ConnectionErrorView()
        {
            InitializeComponent();
            BindingContext = this;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
