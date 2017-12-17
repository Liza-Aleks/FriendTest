using ClassesLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class Methods
    {
        LINQmethods li = new LINQmethods();
        FriendTestEntities db = new FriendTestEntities();

        public List<ResultforOutput> ShowTop(User user)
        {
            List<Result> results = li.FindResult(user);
            List<ResultforOutput> r = new List<ResultforOutput>();
            foreach (var item in results)
            {
             Person per = db.Person.ToList().Find(x => x.ID == item.ID_PersonRespondent);
                r.Add(new ResultforOutput(per.Name, item.Points ));
            }
            return r;
        }
    }
}
