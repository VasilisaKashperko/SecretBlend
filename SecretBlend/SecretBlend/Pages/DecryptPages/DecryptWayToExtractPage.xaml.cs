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
    public partial class DecryptWayToExtractPage : Page
    {
        public DecryptWayToExtractPage()
        {
            InitializeComponent();
            
            if (GlobalClass.isRadioButtonChanged)
            {
                if (!GlobalClass.whatRadioButton)
                {
                    RadioButtonText.IsChecked = true;
                    ShowTextLabel.IsEnabled = true;

                    FileLabel.IsEnabled = false;
                    FileContinueLabel.IsEnabled = false;

                    GlobalClass.whatRadioButton = false;
                }
                if (GlobalClass.whatRadioButton)
                {
                    RadioButtonFile.IsChecked = true;
                    FileLabel.IsEnabled = true;
                    FileContinueLabel.IsEnabled = true;

                    ShowTextLabel.IsEnabled = false;

                    GlobalClass.whatRadioButton = true;
                }
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/DecryptPages/DecryptChooseSecretKeyPage.xaml", UriKind.Relative);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/DecryptPages/DecryptProcessPage.xaml", UriKind.Relative);
        }

        private void RadioButtonText_Click(object sender, RoutedEventArgs e)
        {
            ShowTextLabel.IsEnabled = true;

            FileLabel.IsEnabled = false;
            FileContinueLabel.IsEnabled = false;

            NextButton.IsEnabled = true;

            GlobalClass.whatRadioButton = false;
            GlobalClass.isRadioButtonChanged = true;
        }

        private void RadioButtonFile_Click(object sender, RoutedEventArgs e)
        {
            FileLabel.IsEnabled = true;
            FileContinueLabel.IsEnabled = true;

            ShowTextLabel.IsEnabled = false;

            NextButton.IsEnabled = true;

            GlobalClass.whatRadioButton = true;
            GlobalClass.isRadioButtonChanged = true;
        }

        private void ShowTextLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RadioButtonText.IsChecked = true;
            ShowTextLabel.IsEnabled = true;

            FileLabel.IsEnabled = false;
            FileContinueLabel.IsEnabled = false;

            NextButton.IsEnabled = true;

            GlobalClass.whatRadioButton = false;
            GlobalClass.isRadioButtonChanged = true;
        }

        private void FileLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RadioButtonFile.IsChecked = true;
            FileLabel.IsEnabled = true;
            FileContinueLabel.IsEnabled = true;

            ShowTextLabel.IsEnabled = false;

            NextButton.IsEnabled = true;

            GlobalClass.whatRadioButton = true;
            GlobalClass.isRadioButtonChanged = true;
        }

        private void FileContinueLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RadioButtonFile.IsChecked = true;
            FileLabel.IsEnabled = true;
            FileContinueLabel.IsEnabled = true;

            ShowTextLabel.IsEnabled = false;

            NextButton.IsEnabled = true;

            GlobalClass.whatRadioButton = true;
            GlobalClass.isRadioButtonChanged = true;
        }
    }
}
