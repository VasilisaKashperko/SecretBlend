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
    public partial class EncryptAddMessagePage : Page
    {
        public EncryptAddMessagePage()
        {
            InitializeComponent();
            NextButton.IsEnabled = false;

            if (GlobalClass.TXTFile.ToString() != string.Empty)
            {
                PathLabel.Content = GlobalClass.TXTFile;
                NextButton.IsEnabled = true;
            }

            if (GlobalClass.TXTMessage.ToString() != string.Empty)
            {
                TextBoxMessage.Text = GlobalClass.TXTMessage;
                NextButton.IsEnabled = true;
            }

            if (!GlobalClass.isRadioButtonChanged)
            {
                RadioButtonText.IsChecked = true;
                LableMessage.IsEnabled = true;
                TextBoxMessage.IsEnabled = true;
                TXTDropPanel.IsEnabled = false;
            }

            if (GlobalClass.isRadioButtonChanged)
            {
                RadioButtonFile.IsChecked = true;
                LableMessage.IsEnabled = false;
                TextBoxMessage.IsEnabled = false;
                TXTDropPanel.IsEnabled = true;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/EncryptPages/EncryptChooseSecretKeyPage.xaml", UriKind.Relative);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/EncryptPages/EncryptProcessPage.xaml", UriKind.Relative);
        }

        private void TXTDropPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                GlobalClass.TXTFile = files[files.Length - 1];

                YouChooseLabel.Content = "Вы выбрали файл:";
                PathLabel.Content = GlobalClass.TXTFile;

                NextButton.IsEnabled = true;
            }
        }

        private void ChooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Текстовый файл|*.txt;";
            openFileDialog.ShowDialog();

            GlobalClass.TXTFile = openFileDialog.FileName;

            YouChooseLabel.Content = "Вы выбрали файл:";
            PathLabel.Content = GlobalClass.TXTFile;

            NextButton.IsEnabled = true;
        }

        private void RadioButtonText_Click(object sender, RoutedEventArgs e)
        {
            GlobalClass.isRadioButtonChanged = false;
            GlobalClass.whatRadioButton = false;

            LableMessage.IsEnabled = true;
            TextBoxMessage.IsEnabled = true;

            TXTDropPanel.IsEnabled = false;

            if (GlobalClass.TXTMessage != string.Empty)
            {
                NextButton.IsEnabled = true;
            }
        }

        private void RadioButtonFile_Click(object sender, RoutedEventArgs e)
        {
            GlobalClass.isRadioButtonChanged = true;
            GlobalClass.whatRadioButton = true;

            LableMessage.IsEnabled = false;
            TextBoxMessage.IsEnabled = false;

            TXTDropPanel.IsEnabled = true;

            if (GlobalClass.TXTFile != string.Empty)
            {
                NextButton.IsEnabled = true;
            }
        }

        private void TextBoxMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxMessage.Text.Length > 2)
            {
                GlobalClass.TXTMessage = TextBoxMessage.Text;
                NextButton.IsEnabled = true;
            }
            if (TextBoxMessage.Text.Length < 2)
            {
                GlobalClass.TXTMessage = "";
                NextButton.IsEnabled = false;
            }
        }

        private void Page_MouseMove(object sender, MouseEventArgs e)
        {
            if (!GlobalClass.whatRadioButton)
            {
                if (GlobalClass.TXTMessage == string.Empty)
                {
                    NextButton.IsEnabled = false;
                }
            }
            if (GlobalClass.whatRadioButton)
            {
                if (GlobalClass.TXTFile == string.Empty)
                {
                    NextButton.IsEnabled = false;
                }
            }
        }
    }
}
