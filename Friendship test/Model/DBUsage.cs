using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
     public class DBUsage
    {
        FriendTestEntities db = new FriendTestEntities();
        LINQmethods li = new LINQmethods();

        public List<Person> ShowPerson()
        {
            List<Person> people = new List<Person>();
            people = db.Person.ToList();

            return people;
        }

        public List<Question> ShowQuestions()
        {
            List<Question> questions = new List<Question>();
            questions = db.Question.ToList();

            return questions;
        }

        public void AddPerson(Person p)
        {
            db.Person.Add(p);
            db.SaveChanges();
        }

        public void AddQuestionAboutPerson(Test t)
        {
            db.Test.Add(t);
            db.SaveChanges();
        }

        public void AddQuestion(Question q)
        {
            db.Question.Add(q);
            db.SaveChanges();
        }

        public void AddAnswer(Answer a)
        {
            db.Answer.Add(a);
            db.SaveChanges();
        }

        public void DeleteQuestion(Question que)
        {
            List<Answer> answers =  li.FindAnswers(que.ID);
            foreach (var item in answers)
            {
                db.Answer.Remove(item);
                db.SaveChanges();
            }
            db.Question.Remove(que);
            db.SaveChanges();
        }
    }
}
