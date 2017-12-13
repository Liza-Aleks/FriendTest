using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using ClassesLibrary.Classes;

namespace ClassesLibrary
{
    class VKParser
    {
        public  async Task<IEnumerable<User>> GetFriends(string screenName)
        {
            using (var client = new HttpClient())
            {
                var user = await GetUser(screenName);
                if (user != null)
                {
                    var jsonResponse = await client.GetStringAsync("https://api.vk.com/method/users.get?user_ids=" + screenName);
                    var friends = JsonConvert.DeserializeObject<Response>(jsonResponse);
                    return friends.Users;
                }
                else
                {
                    return null;
                }
            }

        }

        public async Task<User> GetUser(string screenName)
        {
            using (var client = new HttpClient())
            {
                var jsonResponse = await client.GetStringAsync("https://api.vk.com/method/friends.get?user_ids=" + screenName + "&fields=nickname,photo_100");
                var response = JsonConvert.DeserializeObject<Response>(jsonResponse);
                var user = response.Users?[0];
                return user;
            }
        }
        
    }
}
