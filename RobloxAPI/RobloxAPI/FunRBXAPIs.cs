using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace RobloxAPI
{
    public static class FunRBXAPIs
    {

        /// <summary>
        /// Returns an suggested username. If there is no user by the username argument it will just return that.
        /// </summary>
        /// <param name="tryToUsername">Username to try and add onto or keep.</param>
        /// <returns>Returns an suggested</returns>
        public static string GetRecommendedUsername(string tryToUsername)
        {
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead("http://web.roblox.com/UserCheck/GetRecommendedUsername?usernameToTry="+tryToUsername)))
            {
                return reader.ReadToEnd();
            }
        }

        private class SucessObject
        {
            public bool sucesss
            {
                get;
                set;
            }
        }

        public static bool DoesUsernameExist(string username)
        {
            //
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead("http://www.roblox.com/UserCheck/DoesUsernameExist?username="+username)))
            {
                return JsonConvert.DeserializeObject<SucessObject>(reader.ReadToEnd()).sucesss;
            }
        }

    }
}
