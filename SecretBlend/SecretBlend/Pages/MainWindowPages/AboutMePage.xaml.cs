using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class AboutMePage : Page
    {
        public AboutMePage()
        {
            InitializeComponent();
        }

        private void TelegramLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://t.me/vasilisa_kashperko") { UseShellExecute = true });
        }

        private void InstagramLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.instagram.com/vasilisa.kashperko/") { UseShellExecute = true });
        }

        private void VKLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://vk.com/vasilisa_kashperko") { UseShellExecute = true });
        }
    }
}
