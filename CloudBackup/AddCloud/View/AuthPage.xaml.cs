using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Web;
using CloudBackup.Clouds;

namespace CloudBackup.AddCloud.View
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        ICloud cloudController;
        public AuthPage(int cloudId)
        {
            InitializeComponent();
            switch(cloudId)
            {
                case 0://DropBox
                    cloudController = new DropBoxController();
                    break;
            }
        }

        private void WebBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            var uri = cloudController.PrepareUri();
            webBrowser.Navigate(uri);
        }

        private void webBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            var accessToken = cloudController.ParseUriForToken(e.Uri);
            if (accessToken != null)
            {
                // Make a call to /account/info to verify that the access token is working.
                var client = new WebClient();
                client.Headers["Authorization"] = "Bearer " + accessToken;
                var accountInfo = client.DownloadString("https://www.dropbox.com/1/account/info");
                MessageBox.Show("Account info: " + accountInfo);
            }
        }
    }
}
