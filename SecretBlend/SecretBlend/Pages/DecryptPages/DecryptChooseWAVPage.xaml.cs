using Microsoft.Win32;
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

namespace SecretBlend
{
    public partial class DecryptChooseWAVPage : Page
    {
        public DecryptChooseWAVPage()
        {
            InitializeComponent();

            NextButton.IsEnabled = false;

            if (GlobalClass.WAVfile.ToString() != string.Empty)
            {
                YouChooseLabel.Content = "Вы выбрали файл:";
                PathLabel.Content = GlobalClass.WAVfile;
                NextButton.IsEnabled = true;
            }

            GlobalClass.isDecrypt = true;
        }

        private void WAVDropPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                GlobalClass.WAVfile = files[files.Length - 1];

                YouChooseLabel.Content = "Вы выбрали файл:";
                PathLabel.Content = GlobalClass.WAVfile;

                NextButton.IsEnabled = true;
            }
        }

        private void ChooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "WAV-файл|*.wav;";
            openFileDialog.ShowDialog();

            GlobalClass.WAVfile = openFileDialog.FileName;

            YouChooseLabel.Content = "Вы выбрали файл:";
            PathLabel.Content = GlobalClass.WAVfile;

            NextButton.IsEnabled = true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalClass.isDecrypt)
            {
                MessageBoxResult choice = MessageBox.Show("Вы уверены, что хотите закончить процесс шифрования?", "Переход на главную страницу", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (choice)
                {
                    case MessageBoxResult.Yes:
                        this.NavigationService.Source = new Uri("/Pages/MainWindowPages/MainPage.xaml", UriKind.Relative);
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
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalClass.WAVfile.ToString() == string.Empty)
            {
                NextButton.IsEnabled = false;
            }
            else
            {
                this.NavigationService.Source = new Uri("/Pages/DecryptPages/DecryptChooseSecretKeyPage.xaml", UriKind.Relative);
            }
        }

        private void Page_MouseMove(object sender, MouseEventArgs e)
        {
            if (GlobalClass.WAVfile.ToString() == string.Empty)
            {
                NextButton.IsEnabled = false;
            }
        }
    }
}
