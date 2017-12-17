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
    /// Логика взаимодействия для TakeTest.xaml
    /// </summary>
    public partial class TakeTest : Page
    {
        Test t;
        Person p;
        Person friend;
        DBUsage q = new DBUsage();
        LINQmethods li = new LINQmethods();
        int temp = 1;



        public TakeTest(Test test, Person person, Person friendperson)
        {
            t = test;
            p = person;
            friend = friendperson;
            InitializeComponent();

            var questionsinTest = li.FindQuestioninTest(friendperson);
            var questions = li.FindQuestion(questionsinTest);
            
            var question = questions[0];
            List<Answer> answers = li.FindAnswers(question.ID);

            labelQuestion.Content = question.Text;
            listBoxAnswers.ItemsSource = answers;

            buttonNext.Visibility = Visibility.Visible;
            buttonEnd.Visibility = Visibility.Hidden;
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            var questionsinTest = li.FindQuestioninTest(p);
            var questions = li.FindQuestion(questionsinTest);

            var question = questions[temp];
            List<Answer> answers = li.FindAnswers(question.ID);

            labelQuestion.Content = question.Text;
            listBoxAnswers.ItemsSource = answers;

            if(temp == 9)
            {
                buttonNext.Visibility = Visibility.Hidden;
                buttonEnd.Visibility = Visibility.Visible;
            }
            temp++;
        }
    }
}
