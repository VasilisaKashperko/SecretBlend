using System;
using System.Collections.Generic;
using System.Drawing;
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
using Color = System.Windows.Media.Color;

namespace SecretBlend
{
    public partial class MainWindow : Window
    {
        public SolidColorBrush activeButtonBrush = new SolidColorBrush(Color.FromArgb(63, 0, 0, 0));
        public SolidColorBrush inactiveButtonBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void ButtonHide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonMain_Click(object sender, RoutedEventArgs e)
        {
            ButtonMain.Background = activeButtonBrush;
            ButtonAboutMe.Background = inactiveButtonBrush;
            ButtonAboutProject.Background = inactiveButtonBrush;

            FrameContainer.Source = new Uri("/Pages/MainWindowPages/MainPage.xaml", UriKind.Relative);

            ButtonMain.IsEnabled = true;
            ButtonAboutMe.IsEnabled = true;
            ButtonAboutProject.IsEnabled = true;
        }

        private void ButtonAboutProject_Click(object sender, RoutedEventArgs e)
        {
            ButtonAboutProject.Background = activeButtonBrush;
            ButtonAboutMe.Background = inactiveButtonBrush;
            ButtonMain.Background = inactiveButtonBrush;

            FrameContainer.Source = new Uri("/Pages/MainWindowPages/AboutProjectPage.xaml", UriKind.Relative);

            ButtonMain.IsEnabled = true;
            ButtonAboutMe.IsEnabled = true;
            ButtonAboutProject.IsEnabled = true;
        }

        private void ButtonAboutMe_Click(object sender, RoutedEventArgs e)
        {
            ButtonAboutMe.Background = activeButtonBrush;
            ButtonMain.Background = inactiveButtonBrush;
            ButtonAboutProject.Background = inactiveButtonBrush;

            FrameContainer.Source = new Uri("Pages/MainWindowPages/AboutMePage.xaml", UriKind.Relative);

            ButtonMain.IsEnabled = true;
            ButtonAboutMe.IsEnabled = true;
            ButtonAboutProject.IsEnabled = true;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
