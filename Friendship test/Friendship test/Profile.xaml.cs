using Model;
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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Profile : Page
    {
        DBUsage q = new DBUsage();
        FriendTestEntities1 db = new FriendTestEntities1();

        public Profile()
        {

            InitializeComponent();
        }

        private void buttonCreateTest_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
