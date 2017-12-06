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
        FriendTestEntities1 db = new FriendTestEntities1();
        List<Person> people = new List<Person>();
        MainWindow wnd;
        public Registration(MainWindow w)
        {
            wnd = w;
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            int pass = 0;
            people = q.ShowPerson();
            foreach (var item in people)
            {
                if (item.Login == textBoxLogin.Text)
                    MessageBox.Show("Этот логин уже используется");
                else if (item.Vk == textBoxVK.Text)
                    MessageBox.Show("Вы уже зарегистрированы в системе");
                else if (textBoxLogin.Text == "" || textBoxVK.Text == "" || passwordBoxFirst.Password == "" || passwordBoxSecond.Password == "")
                    MessageBox.Show("Все поля должны быть заполнены");
                else if (passwordBoxFirst.Password != passwordBoxSecond.Password)
                    MessageBox.Show("Вы ввели разные пароли");
                else
                {
                    pass = Hashing.makeHash(passwordBoxFirst.Password);
                    Person p = new Person { Login = textBoxLogin.Text, Name = "Имя",Vk=textBoxVK.Text ,Password = pass.ToString(), Test = 1 };
                    q.AddPerson(p);
                    MessageBox.Show("Регистрация пройдена успешно");
                    Test test = new Test();
                    test.Show();
                    wnd.Close();
                }
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            wnd.Registration.Content = new FriendshipTest(wnd);
        }
    }
}
