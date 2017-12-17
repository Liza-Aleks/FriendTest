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
        Model.Methods m = new Model.Methods();
        

        public Profile(Person per, Test tes)
        {
            t = tes;
            p = per;
            InitializeComponent();
            people = q.ShowPerson();
            labelName.Content = p.Name;
            Model.Test tryy = new Model.Test();
           
            if (p.Test == 1)
            {
                buttonAddQuestion.Visibility = Visibility.Visible;
            }

            tryy = db.Test.ToList().Find(x => x.ID_Person == p.ID);
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
                var results = m.ShowTop(userfriend);

                if (listBoxTop.Items.Count > 0)
                    listBoxTop.ItemsSource = (new List<ResultforOutput>());
                
                if (results.Count() > 0)
                    listBoxTop.ItemsSource = results;
                


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

                Person friend = db.Person.ToList().Find(x => x.Vk == userfriend.ScreenName);
                Result friendresult = db.Result.ToList().Find(x => x.ID_PersonQuestioner == friend.ID && x.ID_PersonRespondent == p.ID);
                if (db.Result.ToList().Contains(friendresult))
                {
                    buttonTakeTest.IsEnabled = false;
                }
                else
                    buttonTakeTest.IsEnabled = true;
                Model.Test friendtest = db.Test.ToList().Find(x => x.ID_Person == friend.ID);
                if (db.Test.ToList().Contains(friendtest))
                    buttonTakeTest.Visibility = Visibility.Visible;
                else
                   buttonTakeTest.Visibility = Visibility.Hidden;

                buttonBackToPage.Visibility = Visibility.Visible;
                buttonTakeTest.Visibility = Visibility.Visible;
                buttonCreateTest.Visibility = Visibility.Hidden;
                buttonAddQuestion.Visibility = Visibility.Hidden;
            }
        }

     
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (listBoxTop.Items.Count > 0)
                listBoxTop.ItemsSource = (new List<ResultforOutput>());

            var friends = await VKParser.GetFriends(p.Vk, count:5000);
            List<User> friendsInSystem = new List<User>();
            foreach (var item in friends)
            {
                if (db.Person.ToList().Find(x => x.Vk == item.ScreenName) != null)
                {
                    friendsInSystem.Add(item);
                }
            }
            if (listBoxAllFriends.Items.Count > 0)
                listBoxAllFriends.Items.Clear();

            if (friendsInSystem.Count() > 0)
             listBoxAllFriends.ItemsSource = friendsInSystem;
            
            else
           listBoxAllFriends.ItemsSource = (new List<User>());
           
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

            var results = m.ShowTop(user);
            if (results.Count() > 0)
                listBoxTop.ItemsSource = results;
           

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

            if (listBoxTop.Items.Count > 0)
                listBoxTop.ItemsSource = (new List<ResultforOutput>());

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
