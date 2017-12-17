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


         public List<Test> FindQuestionAboutPerson(Person personq)
        {
            var questionsinTest = (from a in db.Test.ToList()
                                   where a.ID_Person == personq.ID
                                   select a).ToList();
            
            return questionsinTest;
        }
    }
}
