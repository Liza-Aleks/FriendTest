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
        public int a = 0;
        DBUsage q = new DBUsage();
        FriendTestEntities db = new FriendTestEntities();
        Test t;
        Person p;
        public CreateTest(Test tes, Person per)
        {
            t = tes;
            p = per;
            InitializeComponent();
            listBoxQuestions.ItemsSource = db.Question.ToList();
            

        }

        private void listBoxQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxQuestions.SelectedItem != null)
            {
                Question q = (Question)listBoxQuestions.SelectedItem;
                listBoxAnswers.Items.Clear();
                Model.Test tryy = new Model.Test();
                tryy = db.Test.ToList().Find(x => x.ID_Person == p.ID && x.ID_Question == q.ID);
                if (db.Test.ToList().Contains(tryy))
                {
                    MessageBox.Show("Вы уже выбрали этот вопрос!");
                }
                else
                {
                    buttonChoose.IsEnabled = false;
                    listBoxAnswers.Items.Clear();
                    foreach (var item in db.Answer.ToList())
                    {
                        if (q.ID == item.ID_question)
                        {
                            listBoxAnswers.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void listBoxAnswers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listBoxAnswers.SelectedItem !=null)
            {
                buttonChoose.IsEnabled = true;
            }
        }

        private void buttonChoose_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxAnswers.SelectedItem != null && listBoxQuestions.SelectedItem != null)
            {
                if (a != 10)
                {
                    Answer answ = (Answer)listBoxAnswers.SelectedItem;
                    a += 1;
                    Model.Test t = new Model.Test { ID_Person = p.ID , ID_Question = answ.ID_question, ID_Answer = answ.ID};
                    q.AddQuestionAboutPerson(t);
                }
                else
                    MessageBox.Show("Вы уже выбрали 10 вопросов");
            }
            
            if(a==10)
            {
                buttonChoose.IsEnabled = false;
                buttonEnd.IsEnabled = true;
            }



        }

        private void buttonEnd_Click(object sender, RoutedEventArgs e)
        {
            t.Main.Content = new Profile(p,t);
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Завершите тест!!!");
        }
    }
}
