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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace youtubeDownloaderByFedi
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void MyApplicationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GridEventHandelrInitializer();
            MainApp.Visibility = Visibility.Visible;
            Loading.Visibility = Visibility.Hidden;
            Done.Visibility = Visibility.Hidden;

            UrlInput.Text = "";
           
        }
        private void MyApplicationWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
           if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainApp.Visibility = Visibility.Hidden;
            Loading.Visibility = Visibility.Hidden;
            DownloadYoutube downloadYoutube = new DownloadYoutube(UrlInput.Text, settingBar.DirectoryText.Text);
            downloadYoutube.RunDownloder();
        }

        void GridEventHandelrInitializer()
        {
            MainApp.IsVisibleChanged += new DependencyPropertyChangedEventHandler(VisibleChanged);
            Loading.IsVisibleChanged += new DependencyPropertyChangedEventHandler(VisibleChanged);
            Done.IsVisibleChanged += new DependencyPropertyChangedEventHandler(VisibleChanged);
        }

        void VisibleChanged( object sender, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = (Grid)sender;
            RightTransition("TransitionAnimation", grid);
            
        }

        /// <summary>
        /// Animate The grid from the Right To The Left
        /// </summary>
        /// <param name="storyBoard"></param>
        /// <param name="targetControl"></param>
        void RightTransition(string storyBoard, Grid targetControl)
        {
            Storyboard transitionStoryBoard = Resources[storyBoard] as Storyboard;
            targetControl.BeginStoryboard(transitionStoryBoard);
        }
    }
}
