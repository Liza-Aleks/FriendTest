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
    public class VKParser
    {
 
        private const string _apiUrl = "https://api.vk.com/method/";

        private string GetUrl(string methodName) => _apiUrl + methodName;

        private string GetUserUrl(string screenName) => $"{GetUrl("users.get")}?user_ids={screenName}";

        private string GetFriendsUrl(int id, int count)
        {
            return $"{GetUrl("friends.get")}?user_id={id}&count={count}&fields=nickname,photo_100";
        }
        private string GetInfoUrl(string screenName) => _apiUrl + $"users.get?user_ids={screenName}&fields=photo_100,status,online";
        public async Task<User> GetUser(string screenName)
        {
            using (var client = new HttpClient())
            {
                var jsonResponse = await client.GetStringAsync(GetUserUrl(screenName));
                var response = JsonConvert.DeserializeObject<Response>(jsonResponse);
               
                var user = response.Users?[0];
                return user;
            }
        }

        public async Task<IEnumerable<User>> GetFriends(string screenName, int count = 0)
        {
            using (var client = new HttpClient())
            {
                var user = await GetUser(screenName);

              
                if (user != null)
                {
                    var jsonResponse = await client.GetStringAsync(GetFriendsUrl(user.Id, count));
                    var friends = JsonConvert.DeserializeObject<Response>(jsonResponse);

                    return friends.Users;
                }
                else
                    return null;
            }
        }
        public async Task<User> GetUserInfo(string screenName)
        {
            using (var client = new HttpClient())
            {
                var jsonResponse = await client.GetStringAsync(GetInfoUrl(screenName));
                var response = JsonConvert.DeserializeObject<Response>(jsonResponse);

                var user = response.Users?[0];
                return user;
            }
        }
        
    }
}
