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
        Question question = new Question();
        Answer answer = new Answer();
        public AddQuestion()
        {
            InitializeComponent();
            listBoxQuestions.ItemsSource = db.Question.ToList();
        }

        private void buttonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxQuestion.Text.Length<10)
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
            Model.Question question = new Model.Question { Text = textBoxQuestion.Text };
            q.AddQuestionToDB(question);
            int id = -1;

            foreach (var item in db.Question.ToList())
            {
                if (item.Text == textBoxQuestion.Text)
                {
                    id = item.ID;
                    break;
                }
            }

            if (id != -1)
            {
                foreach (var item in listBoxAnswers.Items)
                {
                    Model.Answer answer = new Model.Answer { ID_question = id, Text = listBoxAnswers.Items.ToString() };
                }
            }
        }
    }
}
