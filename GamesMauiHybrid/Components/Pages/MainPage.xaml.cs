using GamesBlazor.Shared.States;

namespace GamesMauiHybrid
{
    public partial class MainPage : ContentPage
    {
        private ActivityIndicatorState ActivityIndicatorState { get; set; }
        public MainPage(ActivityIndicatorState counterState)
        {
            InitializeComponent();

            ActivityIndicatorState = counterState;
            BindingContext = ActivityIndicatorState;
        }


    }
}
