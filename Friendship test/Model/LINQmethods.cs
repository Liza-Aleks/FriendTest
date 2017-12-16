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
        

    }
}
