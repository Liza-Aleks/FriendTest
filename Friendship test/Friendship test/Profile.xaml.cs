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
                string temp = userfriend.FirstName + " " + userfriend.LastName; 
                Person friend = db.Person.ToList().Find(x => x.Name == temp);
           
                label.Name = friend.Name;
                listBoxTop.ItemsSource = db.Result.ToList();
            }
        }

     
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            listBoxAllFriends.Items.Clear();
            listBoxAllFriends.ItemsSource = await VKParser.GetFriends(p.Vk, count:5000);
            var user = await VKParser.GetUserInfo(p.Vk);
         
            image.Source = new BitmapImage(new Uri(user.PhotoUrl));
            
        }
    }
}
