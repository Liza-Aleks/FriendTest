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
using ClassesLibrary;
using ClassesLibrary.Classes;

namespace Friendship_test
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Profile : Page
    {
        Test t;
        DBUsage q = new DBUsage();
        LINQmethods li = new LINQmethods();
        FriendTestEntities db = new FriendTestEntities();
        List<Person> people = new List<Person>();
        Person p = new Person();
        VKParser VKParser = new VKParser();
        

        public Profile(Person per, Test tes)
        {
            t = tes;
            p = per;
            InitializeComponent();
            people = q.ShowPerson();
            labelName.Content = p.Name;
            Model.Test tryy = new Model.Test();
            tryy = db.Test.ToList().Find(x => x.ID_Person==p.ID);
            if (p.Test == 1)
            {
                buttonAddQuestion.Visibility = Visibility.Visible;
            }
            if (db.Test.ToList().Contains(tryy))
             buttonCreateTest.Visibility = Visibility.Hidden;
            else
             buttonCreateTest.Visibility = Visibility.Visible;
        }

        private void buttonCreateTest_Click(object sender, RoutedEventArgs e)
        {
            t.Main.Content = new CreateTest(t,p);
        }

        private void listBoxAllFriends_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonGoTo.IsEnabled = true;
        }

        private void buttonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            AddQuestion wnd = new AddQuestion();
            wnd.Show();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            t.Close();
        }

        private void buttonGoTo_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxAllFriends.SelectedItem != null)
            {
                User userfriend = (User)listBoxAllFriends.SelectedItem;
                List<Result> results = li.FindResult(userfriend);
                listBoxTop.Items.Clear();
                if (results.Count() > 0)
                {
                    listBoxTop.ItemsSource = results;
                }
                else
                {
                    listBoxTop.Items.Add("Никто не прошел тест");
                }

                string tempname = userfriend.FirstName + " " + userfriend.LastName;
                image.Source = new BitmapImage(new Uri(userfriend.PhotoUrl));

                labelName.Content = tempname;
                if (userfriend.Status == "")
                {
                    labelS.Visibility = Visibility.Hidden;
                    labelStatus.Content = "";
                }
                else
                {
                    labelS.Visibility = Visibility.Visible;
                    labelStatus.Content = userfriend.Status;
                }

                if (userfriend.Online == 1)
                {
                    labelOnline.Content = "Онлайн";
                }
                else
                {
                    labelOnline.Content = "Оффлайн";
                }

                buttonBackToPage.Visibility = Visibility.Visible;
                buttonTakeTest.Visibility = Visibility.Visible;
                buttonCreateTest.Visibility = Visibility.Hidden;
                buttonAddQuestion.Visibility = Visibility.Hidden;
            }
        }

     
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            listBoxAllFriends.Items.Clear();
            var friends = await VKParser.GetFriends(p.Vk, count:5000);
            List<User> friendsInSystem = new List<User>();
            foreach (var item in friends)
            {
                if (db.Person.ToList().Find(x => x.Vk == item.ScreenName) != null)
                {
                    friendsInSystem.Add(item);
                }
            }
            if (friendsInSystem.Count() > 0)
            {
                listBoxAllFriends.ItemsSource = friendsInSystem;
            }
            else
                listBoxAllFriends.Items.Add("Нет друзей в системе");
           
            var user = await VKParser.GetUserInfo(p.Vk);
            if (user.Status == "")
            {
                labelS.Visibility = Visibility.Hidden;
                labelStatus.Content = "";
            }
            else
            {
                labelS.Visibility = Visibility.Visible;
                labelStatus.Content = user.Status;
            }

            if (user.Online == 1)
            labelOnline.Content = "Онлайн";
            else
              labelOnline.Content = "Оффлайн";

            List<Result> results = li.FindResult(user);
            listBoxTop.Items.Clear();
            if (results.Count() > 0)
                listBoxTop.ItemsSource = results;
            else
                listBoxTop.Items.Add("Никто не прошел тест");

            image.Source = new BitmapImage(new Uri(user.PhotoUrl));
            
        }

        private async void buttonBackToPage_Click(object sender, RoutedEventArgs e)
        {
            labelName.Content = p.Name;
            var user = await VKParser.GetUserInfo(p.Vk);
            image.Source = new BitmapImage(new Uri(user.PhotoUrl));

            if (user.Status == "")
            {
                labelS.Visibility = Visibility.Hidden;
                labelStatus.Content = "";
            }
            else
            {
                labelS.Visibility = Visibility.Visible;
                labelStatus.Content = user.Status;
            }

            if (user.Online == 1)
                labelOnline.Content = "Онлайн";
            else
                labelOnline.Content = "";

            List<Result> results = li.FindResult(user);
            listBoxTop.Items.Clear();
            listBoxTop.ItemsSource = results;

            Model.Test tryy = new Model.Test();
            tryy = db.Test.ToList().Find(x => x.ID_Person == p.ID);
            if (db.Test.ToList().Contains(tryy))
                buttonCreateTest.Visibility = Visibility.Hidden;
            else
                buttonCreateTest.Visibility = Visibility.Visible;

            buttonBackToPage.Visibility = Visibility.Hidden;
            buttonTakeTest.Visibility = Visibility.Hidden;
            if (p.Test == 1)
            {
                buttonAddQuestion.Visibility = Visibility.Visible;
            }
        }

        private void buttonTakeTest_Click(object sender, RoutedEventArgs e)
        {
            User userfriend = (User)listBoxAllFriends.SelectedItem;
            Person friend = db.Person.ToList().Find(x => x.Vk == userfriend.ScreenName);
            t.Main.Content = new TakeTest(t,p,friend);

        }
    }
}
