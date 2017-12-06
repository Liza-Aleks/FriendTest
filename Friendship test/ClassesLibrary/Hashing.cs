using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesLibrary
{
    public class Hashing
    {
        private const int base_ = 2;
        private const int mod = 1000000007;
        public static int makeHash(string password)
        {
            int ans = 0;
            int p = 1;
            for (int i = 0; i < password.Length; i++)
            {
                int x = password[i];
                ans = (ans + p * x) % mod;
                p = (p * base_) % mod;
            }
            return ans;
        }
    }
}