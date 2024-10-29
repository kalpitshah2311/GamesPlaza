
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace GamesBlazor.Shared.States {
    public class ActivityIndicatorState : INotifyPropertyChanged
    {
        private bool _isActivityIndicatorRunning;
        private bool _isRefreshing;
        private bool _internetConnected;
        public bool IsActivityIndicatorRunning
        {
            get { return _isActivityIndicatorRunning; }
            set
            {
                _isActivityIndicatorRunning = value;
                Notify(nameof(IsActivityIndicatorRunning));
            }
        }
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                Notify(nameof(IsRefreshing));
            }
        }
        public bool InternetConnected
        {
            get { return _internetConnected; }
            set
            {
                _internetConnected = value;
                Notify(nameof(InternetConnected));
            }
        }
        private void Notify([CallerMemberName] string propertyName = "")
        {
            PropertyChanged!.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
