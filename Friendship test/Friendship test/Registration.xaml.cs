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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        DBUsage q = new DBUsage();
        List<Person> people = new List<Person>();
        MainWindow wnd;
        VKParser VKParser = new VKParser();

        public Registration(MainWindow w)
        {
            wnd = w;
            InitializeComponent();
            textBoxLogin.Focus();
        }

        private async void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            int pass = 0;
            bool flag = false;
            people = q.ShowPerson();
            foreach (var item in people)
            {
                if (item.Login == textBoxLogin.Text)
                {
                    MessageBox.Show("Этот логин уже используется");
                    flag = true;
                }

                else if (item.Vk == textBoxVK.Text)
                {
                    MessageBox.Show("Вы уже зарегистрированы в системе");
                    flag = true;
                }
                else if (textBoxLogin.Text == "" || textBoxVK.Text == "" || passwordBoxFirst.Password == "" || passwordBoxSecond.Password == "")
                {
                    MessageBox.Show("Все поля должны быть заполнены");
                    flag = true;
                }
                else if (passwordBoxFirst.Password != passwordBoxSecond.Password)
                {
                    MessageBox.Show("Вы ввели разные пароли");
                    flag = true;
                }
            }
            if (flag == false)
            {
                pass = Hashing.makeHash(passwordBoxFirst.Password);
                var user = await VKParser.GetUser(textBoxVK.Text);
                string newname =  user.FirstName + " " + user.LastName;
                Person p = new Person { Login = textBoxLogin.Text, Name = newname, Vk = textBoxVK.Text, Password = pass.ToString(), Test = 1 };
                q.AddPerson(p);
                MessageBox.Show("Регистрация пройдена успешно");
                Test test = new Test(p);
                test.Show();
                wnd.Close();
            }
            
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            wnd.Registration.Content = new FriendshipTest(wnd);
        }

        private void textBoxLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                passwordBoxFirst.Focus();
            }
        }

        private void passwordBoxFirst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                passwordBoxSecond.Focus();
            }
        }

        private void passwordBoxSecond_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textBoxVK.Focus();
            }
        }

        private void textBoxVK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                buttonOK_Click(sender, e);
            }
        }
    }
}
