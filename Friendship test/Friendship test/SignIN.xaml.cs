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
    /// Логика взаимодействия для SignIN.xaml
    /// </summary>
    public partial class SignIN : Page
    {
        DBUsage q = new DBUsage();
        FriendTestEntities1 db = new FriendTestEntities1();
        List<Person> people = new List<Person>();
        MainWindow wnd;
        public SignIN(MainWindow w)
        {
            wnd = w;
            InitializeComponent();
        }

        private void buttonSignIN_Click(object sender, RoutedEventArgs e)
        {
            
            string a = "wd";
            Test t = new Test();
            t.Show();
            wnd.Close();


            foreach (var item in people)
            {
                if (item.Login == a && item.Password == a)
                {
                    // Test wnd = new Test(this);
                    // wnd.Show();
                }

                else
                    MessageBox.Show("Your login or passoword is incorrect");
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            wnd.Registration.Content = new FriendshipTest(wnd);
        }
    }
}
