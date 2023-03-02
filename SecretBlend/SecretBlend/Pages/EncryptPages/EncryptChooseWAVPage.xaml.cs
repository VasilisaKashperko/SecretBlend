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
    public partial class EncryptChooseWAVPage : Page
    {
        public EncryptChooseWAVPage()
        {
            InitializeComponent();

            if(GlobalClass.WAVfile.ToString() != string.Empty)
            {
                YouChooseLabel.Content = "Вы выбрали файл:";
                PathLabel.Content = GlobalClass.WAVfile;
            }

            GlobalClass.isEncrypt = true;
        }

        private void WAVDropPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                GlobalClass.WAVfile = files[files.Length-1];

                YouChooseLabel.Content = "Вы выбрали файл:";
                PathLabel.Content = GlobalClass.WAVfile;
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
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalClass.isEncrypt)
            {
                MessageBoxResult choice = MessageBox.Show("Вы уверены, что хотите закончить процесс шифрования?", "Переход на главную страницу", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                switch (choice)
                {
                    case MessageBoxResult.Yes:
                        this.NavigationService.Source = new Uri("/Pages/MainWindowPages/MainPage.xaml", UriKind.Relative);
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
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/EncryptPages/EncryptChooseSecretKeyPage.xaml", UriKind.Relative);
        }
    }
}
