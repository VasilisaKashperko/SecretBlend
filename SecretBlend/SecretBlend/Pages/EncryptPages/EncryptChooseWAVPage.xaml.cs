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

            if(GlobalClass.file.ToString() != "")
            {
                YouChooseLabel.Content = "Вы выбрали файл:";
                PathLabel.Content = GlobalClass.file;
            }
        }

        private void WAVDropPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                GlobalClass.file = files[files.Length-1];

                YouChooseLabel.Content = "Вы выбрали файл:";
                PathLabel.Content = GlobalClass.file;
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

            GlobalClass.file = openFileDialog.FileName;

            YouChooseLabel.Content = "Вы выбрали файл:";
            PathLabel.Content = GlobalClass.file;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/MainWindowPages/MainPage.xaml", UriKind.Relative);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/EncryptPages/EncryptChooseSecretKeyPage.xaml", UriKind.Relative);
        }
    }
}
