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
using CloudBackup.AddCloud.View;
using System.Reflection;

namespace CloudBackup.AddCloud.View
{
    /// <summary>
    /// Interaction logic for SelectCloudPage.xaml
    /// </summary>
    public partial class SelectCloudPage : Page
    {
        public SelectCloudPage()
        {
            InitializeComponent();
        }

        private void buttonNextAuthentification_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new AuthPage(listBox.SelectedIndex));
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonNextAuthentification.IsEnabled = true;
        }
    }
}
