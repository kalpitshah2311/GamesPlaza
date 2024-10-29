using GamesBlazor.Shared.States;

namespace GamesMauiHybrid
{
    public partial class App : Application
    {
        public App(ActivityIndicatorState activityIndicatorState)
        {
            InitializeComponent();

            MainPage = new MainPage(activityIndicatorState);
        }
    }
}
