using ClassesLibrary;
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
        List<Person> people = new List<Person>();
        MainWindow wnd;
        public SignIN(MainWindow w)
        {
            wnd = w;
            InitializeComponent();
            textBoxLogin.Focus();
        }

        private void buttonSignIN_Click(object sender, RoutedEventArgs e)
        {
            int pass = 0;
            Person p = new Person();
            pass = Hashing.makeHash(passwordBox.Password);
            people = q.ShowPerson();
            bool a = false;
            foreach (var item in people)
            {  
                if (item.Login == textBoxLogin.Text && item.Password == pass.ToString())
                {
                    p = item;
                    Test t = new Test(p);
                    t.Show();
                    wnd.Close();
                    a = true;
                }
            }

            if (a == false)
                MessageBox.Show("Неправильный логин или пароль");
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            wnd.Registration.Content = new FriendshipTest(wnd);
        }

        private void textBoxLogin_KeyDown(object sender, KeyEventArgs e)

        {
            if (e.Key == Key.Enter)
            {
                passwordBox.Focus();
            }
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                buttonSignIN_Click(sender, e);
            }
        }
    }
}
