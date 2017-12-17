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
    /// Логика взаимодействия для AddQuestion.xaml
    /// </summary>
    public partial class AddQuestion : Window
    {
        FriendTestEntities db = new FriendTestEntities();
        DBUsage q = new DBUsage();
        LINQmethods li = new LINQmethods();
        Question question = new Question();
        Answer answer = new Answer();
        public AddQuestion()
        {
            InitializeComponent();
            listBoxQuestions.ItemsSource = db.Question.ToList();
        }

        private void buttonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            bool f = false;
            foreach (var item in db.Question.ToList())
            {
                if (item.Text == textBoxQuestion.Text)
                {
                    f = true;
                    MessageBox.Show("Данный вопрос уже есть в системе!");
                    textBoxQuestion.Clear();
                    break;
                }
            }
            if (f == false)
            {
                if (textBoxQuestion.Text.Length < 10)
                {
                    MessageBox.Show("Введите нормальный вопрос!");
                }
                else
                {
                    buttonAddQuestion.IsEnabled = false;
                    buttonAddAnswer.IsEnabled = true;
                    textBoxQuestion.IsEnabled = false;
                }
            }
        }

        private void buttonAddAnswer_Click(object sender, RoutedEventArgs e)
        {
            string answer;
            answer = textBoxAnswer.Text;
            listBoxAnswers.Items.Add(answer);
            textBoxAnswer.Clear();
            if (listBoxAnswers.Items.Count>=2)
            {
                buttonEnd.IsEnabled = true;
            }
        }

        private void buttonEnd_Click(object sender, RoutedEventArgs e)
        {
           Question question = new Question { Text = textBoxQuestion.Text };
            q.AddQuestion(question);
            int id = li.CheckQuestion(textBoxQuestion.Text);
             
                foreach (var item in listBoxAnswers.Items)
                {
                    Answer answer = new Answer { ID_question = question.ID, Text = item.ToString() };
                    q.AddAnswer(answer);
                }
            

            textBoxQuestion.Clear();
            textBoxAnswer.Clear();
            textBoxQuestion.IsEnabled = true;
            textBoxAnswer.IsEnabled = false;
            buttonAddQuestion.IsEnabled = true;
            buttonAddAnswer.IsEnabled = false;
            buttonEnd.IsEnabled = false;
            listBoxAnswers.Items.Clear();
            listBoxQuestions.ItemsSource = db.Question.ToList();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Question que = (Question)listBoxQuestions.SelectedItem;
            if (listBoxQuestions.SelectedItem != null)
            {
                if (db.Test.FirstOrDefault(ee => ee.ID_Question == que.ID) == null)
                {
                    listBoxQuestions.SelectedItem = null;
                    q.DeleteQuestion(que);
                    listBoxQuestions.ItemsSource = db.Question.ToList();
                }
                else
                    MessageBox.Show("Вы не можете удалить этот вопрос");
            }
            else
                MessageBox.Show("Выберите вопрос");
        }
    }
}
