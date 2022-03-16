using System.Windows.Media;


namespace ChatClient.ViewModels
{
    public class MainWindowFields : Base.ViewModel
    {


        public struct LightTheme
        {
            public static readonly SolidColorBrush background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            public static readonly SolidColorBrush foreground = new SolidColorBrush(Color.FromRgb(36, 36, 36));
            public static readonly SolidColorBrush title = new SolidColorBrush(Color.FromRgb(225, 225, 225));
            public static readonly string iconTheme = "MoonAndStars";
        }

        public struct DarkTheme
        {
            public static readonly SolidColorBrush background = new SolidColorBrush(Color.FromRgb(36, 36, 36));
            public static readonly SolidColorBrush foreground = new SolidColorBrush(Color.FromRgb(225, 225, 225));
            public static readonly SolidColorBrush title = new SolidColorBrush(Color.FromRgb(50, 50, 50));
            public static readonly string iconTheme = "WeatherSunny";
        }
    }
}
