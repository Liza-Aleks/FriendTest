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
    /// Логика взаимодействия для CreateTest.xaml
    /// </summary>

    
    public partial class CreateTest : Page
    {

        DBUsage q = new DBUsage();
        FriendTestEntities db = new FriendTestEntities();
        List<Question> questions = new List<Question>();
        public CreateTest()
        {
            questions = q.ShowQuestions();
            listBoxQuestions.ItemsSource = questions;
            InitializeComponent();

        }
    }
}
