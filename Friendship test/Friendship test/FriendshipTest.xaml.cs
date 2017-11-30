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

namespace Friendship_test
{
    /// <summary>
    /// Логика взаимодействия для FriendshipTest.xaml
    /// </summary>
    public partial class FriendshipTest : Page
    {
        MainWindow wnd;

        public FriendshipTest(MainWindow w)
        {
            wnd = w;
            InitializeComponent();
        }

        private void buttonSignIN_Click(object sender, RoutedEventArgs e)
        {
            wnd.Registration.Content = new SignIN();
        }

        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            wnd.Registration.Content = new Registration();
        }
    }
}
