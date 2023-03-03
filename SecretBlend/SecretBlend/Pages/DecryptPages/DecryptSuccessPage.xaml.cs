using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class DecryptSuccessPage : Page
    {
        public DecryptSuccessPage()
        {
            InitializeComponent();

            FileLabel.Visibility = Visibility.Collapsed;
            PathIntroLabel.Visibility = Visibility.Collapsed;
            PathLabel.Visibility = Visibility.Collapsed;

            TextLabel.Visibility = Visibility.Collapsed;
            TextBlock.Visibility = Visibility.Collapsed;

            if (!GlobalClass.whatRadioButton)
            {
                TextLabel.Visibility = Visibility.Visible;
                TextBlock.Visibility = Visibility.Visible;

                string txtFile = File.ReadAllText(GlobalClass.ExtractFileResult);

                GlobalClass.ExtractResult = txtFile;

                File.Delete(GlobalClass.ExtractFileResult);

                TextBlock.Text = txtFile;
            }

            if (GlobalClass.whatRadioButton)
            {
                FileLabel.Visibility = Visibility.Visible;
                PathIntroLabel.Visibility = Visibility.Visible;
                PathLabel.Visibility = Visibility.Visible;

                PathLabel.Content = GlobalClass.ExtractFileResult;
            }
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/MainWindowPages/MainPage.xaml", UriKind.Relative);
            GlobalClass.isDecrypt = false;
            GlobalClass.isRadioButtonChanged = false;
            GlobalClass.whatRadioButton = false;
            GlobalClass.WAVfile = "";
            GlobalClass.secretKey = "";
            GlobalClass.ExtractFileResult = "";
            GlobalClass.ExtractResult = "";
        }

        private void TextBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBlock.Text = GlobalClass.ExtractResult;
        }

        private void TextBlock_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBlock.Text = GlobalClass.ExtractResult;
        }
    }
}
