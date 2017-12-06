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
using System.Windows.Shapes;

namespace Friendship_test
{
    /// <summary>
    /// Логика взаимодействия для Test.xaml
    /// </summary>

    public partial class Test : Window
    {
        DBUsage q = new DBUsage();
        FriendTestEntities1 db = new FriendTestEntities1();
       public Person per = new Person();
       
        public Test(Person p)
        {
            per = p;
            InitializeComponent();
            Main.Content = new Profile(per);

            
         
        }



    }
}
