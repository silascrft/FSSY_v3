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

namespace FSSY_v3
{
    //TODO Implement saving to pathsManager
    public partial class PathOverlay : Page
    {
        public PathOverlay()
        {
            InitializeComponent();
        }

        private void HandleClose(object sender, RoutedEventArgs e)
        {
            // NavigationService.GoBack();
            var mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.GetMenuPage().GetModalFrame().Navigate(null);
        }
    }
}
