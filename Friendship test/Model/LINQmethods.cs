using ClassesLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

   public class LINQmethods
    {
        FriendTestEntities db = new FriendTestEntities();
        

        public int CheckQuestion(string text)
        {
            var que = (from a in db.Question.ToList()
                       where a.Text == text
                       select a).ToList();
            Question question = que.First();
            int id = question.ID;
            return id;

        }

        public static bool CheckIfTestPassed(int idPerson, int idCreator)
        {
            using (var db = new FriendTestEntities())
            {
                return db.Result.Any(r => r.ID_PersonQuestioner == idCreator && r.ID_PersonRespondent == idPerson);
            }
        }
        public static bool CheckIfTestCreated(int idPerson)
        {
            using (var db = new FriendTestEntities())
            {
                return db.Test.Any(t => t.ID_Person == idPerson);
            }
        }

        public static void RemoveQuestion(Question que)
        {
            using (var db = new FriendTestEntities())
            {
                int id = que.ID;
                var answers = from ans in db.Answer
                              where ans.ID_question == id
                              select ans;
                foreach (Answer item in answers)
                {
                    db.Answer.Remove(item);
                }
                db.SaveChanges();
                var quest = from q in db.Question
                            where q.ID == id
                            select q;
                foreach (Question q in quest)
                {
                    db.Question.Remove(q);
                }
                db.SaveChanges();
            }
        }
        public List<Answer> FindAnswers(int idquestion)
        {
            var listanswers = (from a in db.Answer.ToList()
                               where a.ID_question == idquestion
                               select a).ToList();
            
            return listanswers;
        }

        public List<Result> FindResult(User user)
        {
            Person friend = db.Person.ToList().Find(x => x.Vk == user.ScreenName);
            var results = (from a in db.Result.ToList()
                           where a.ID_PersonQuestioner == friend.ID
                           select a).ToList();

            return results;
        }


         public List<Test> FindQuestioninTest(Person personq)
        {
            var questionsinTest = (from a in db.Test.ToList()
                                   where a.ID_Person == personq.ID
                                   orderby a.ID_Question
                                   select a).ToList();
          
            return questionsinTest;
        }

        public List<Question> FindQuestion(List<Test> questionsinTest)
        {
            List<Question> qu = new List<Question>();
            foreach (var item in questionsinTest)
            {
                Question question= db.Question.ToList().Find(x => x.ID == item.ID_Question);
                qu.Add(question);
            }

            var questions = (from a in qu
                             orderby a.ID
                             select a).ToList();
                        


            return questions;
        }
    }
}
