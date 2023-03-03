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

            if (GlobalClass.isEncrypt == true)
            {
                ButtonAboutProject.IsEnabled = false;
                ButtonAboutMe.IsEnabled = false;
            }
            if (!GlobalClass.isEncrypt && !GlobalClass.isDecrypt)
            {
                ButtonAboutProject.IsEnabled = true;
                ButtonAboutMe.IsEnabled = true;
            }
            if (GlobalClass.isDecrypt == true)
            {
                ButtonAboutProject.IsEnabled = false;
                ButtonAboutMe.IsEnabled = false;
            }
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

            if (GlobalClass.isEncrypt)
            {
                MessageBoxResult choice = MessageBox.Show("Вы уверены, что хотите закончить процесс шифрования?", "Переход на главную страницу", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (choice)
                {
                    case MessageBoxResult.Yes:
                        FrameContainer.Source = new Uri("/Pages/MainWindowPages/MainPage.xaml", UriKind.Relative);
                        GlobalClass.isEncrypt = false;
                        GlobalClass.isRadioButtonChanged = false;
                        GlobalClass.whatRadioButton = false;
                        GlobalClass.WAVfile = "";
                        GlobalClass.secretKey = "";
                        GlobalClass.TXTFile = "";
                        GlobalClass.TXTMessage = "";
                        GlobalClass.EncryptedWAVFile = "";

                        break;

                    case MessageBoxResult.No:

                        break;
                }
            }

            if (GlobalClass.isDecrypt)
            {
                MessageBoxResult choice = MessageBox.Show("Вы уверены, что хотите закончить процесс шифрования?", "Переход на главную страницу", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (choice)
                {
                    case MessageBoxResult.Yes:
                        FrameContainer.Source = new Uri("/Pages/MainWindowPages/MainPage.xaml", UriKind.Relative);
                        GlobalClass.isDecrypt = false;
                        GlobalClass.isRadioButtonChanged = false;
                        GlobalClass.whatRadioButton = false;
                        GlobalClass.WAVfile = "";
                        GlobalClass.secretKey = "";
                        GlobalClass.ExtractFileResult = "";
                        GlobalClass.ExtractResult = "";

                        break;

                    case MessageBoxResult.No:

                        break;
                }
            }

            if (!GlobalClass.isEncrypt && !GlobalClass.isDecrypt)
            {
                FrameContainer.Source = new Uri("/Pages/MainWindowPages/MainPage.xaml", UriKind.Relative);
            }
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

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (GlobalClass.isEncrypt)
            {
                ButtonAboutProject.IsEnabled = false;
                ButtonAboutMe.IsEnabled = false;
            }
            if (!GlobalClass.isEncrypt)
            {
                ButtonAboutProject.IsEnabled = true;
                ButtonAboutMe.IsEnabled = true;
            }
            if (GlobalClass.isDecrypt)
            {
                ButtonAboutProject.IsEnabled = false;
                ButtonAboutMe.IsEnabled = false;
            }
            if (!GlobalClass.isDecrypt)
            {
                ButtonAboutProject.IsEnabled = true;
                ButtonAboutMe.IsEnabled = true;
            }
        }
    }
}
