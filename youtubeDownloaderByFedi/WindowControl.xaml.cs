using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace youtubeDownloaderByFedi
{
    /// <summary>
    /// Logique d'interaction pour WindowControl.xaml
    /// </summary>
    public partial class WindowControl : UserControl
    {
        public WindowControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.WindowState = WindowState.Minimized;
        }

       
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserControl bar = ((MainWindow)System.Windows.Application.Current.MainWindow).settingBar;
            AnimateBar("ShowSettingspnl", settings, bar);

        }

        bool animateState = true;
        private void AnimateBar(string storyBoard, TextBlock btn, UserControl settingBar)
        {
            animateState = !animateState;
            if(animateState == false)
            {
                Storyboard sb = Resources["ShowSettingspnl"] as Storyboard;
                sb.Begin(settingBar);
            }
            else
            {
                Storyboard sb = Resources["HideSettingspnl"] as Storyboard;
                sb.Begin(settingBar);
            }
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).MyApplicationWindow_Loaded(sender, e);
        }
    }
}
