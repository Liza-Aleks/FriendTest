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

        public void AddQuestionToDB(Question q)
        {
            db.Question.Add(q);
            db.SaveChanges();
        }

        public void AddAnswerToDB(Answer a)
        {
            db.Answer.Add(a);
            db.SaveChanges();
        }

        public void DeleteQuestionFromDB(Question q)
        {
            db.Question.Remove(q);
            db.SaveChanges();
        }

        public void DeleteAnswerFromDB(Answer a)
        {
            db.Answer.Remove(a);
            db.SaveChanges();
        }
    }
}
