using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class DBUsage
    {

        FriendTestEntities1 db = new FriendTestEntities1();

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

        public void AddPerson(Person per)
        {
            db.Person.Add(per);
            db.SaveChanges();
        }
    }
}
