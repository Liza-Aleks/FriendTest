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
        FriendTestEntities db = new FriendTestEntities();
        int temp = 1;
        int points = 0;


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
            if (listBoxAnswers.SelectedItem != null)
            {
                var questionsinTest = li.FindQuestioninTest(friend);
                var questions = li.FindQuestion(questionsinTest);

                Answer friendanswer = (Answer)listBoxAnswers.SelectedItem;
                Model.Test correct = questionsinTest.Find(x => x.ID_Question == friendanswer.ID_question);
                Answer correctansw = db.Answer.ToList().Find(x => x.ID == correct.ID_Answer);
                string correctanswer = "Правильный ответ: " + correctansw.Text;
                if (friendanswer.ID == correct.ID_Answer)
                {
                    MessageBox.Show("Правильно!!!");
                    points++;
                }
                else
                    MessageBox.Show(correctanswer);
               
                var question = questions[temp];
                List<Answer> answers = li.FindAnswers(question.ID);

                labelQuestion.Content = question.Text;
                listBoxAnswers.ItemsSource = answers;

                buttonNext.IsEnabled = false;

                if (temp == 9)
                {
                    buttonNext.Visibility = Visibility.Hidden;
                    buttonEnd.Visibility = Visibility.Visible;
                }
                temp++;
            }
        }

        private void buttonEnd_Click(object sender, RoutedEventArgs e)
        {

            var questionsinTest = li.FindQuestioninTest(friend);
            var questions = li.FindQuestion(questionsinTest);

            Answer friendanswer = (Answer)listBoxAnswers.SelectedItem;
            Model.Test correct = questionsinTest.Find(x => x.ID_Question == friendanswer.ID_question);
            Answer correctansw = db.Answer.ToList().Find(x => x.ID == correct.ID_Answer);
            string correctanswer = "Правильный ответ: "+ correctansw.Text;
            if (friendanswer.ID == correct.ID_Answer)
            {
                MessageBox.Show("Правильно!!!");
                points++;
            }
            else
                MessageBox.Show(correctanswer);

            string point = "Вы набрали " + points;
            MessageBox.Show(point);
            Result result = new Result { ID_PersonQuestioner = friend.ID, ID_PersonRespondent = p.ID, Points = points };
            q.AddResult(result);
            t.Main.Content = new Profile(p,t);



        }

        private void listBoxAnswers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonNext.IsEnabled = true;
        }
    }
}
