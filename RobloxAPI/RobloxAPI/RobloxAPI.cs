using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Drawing;
using HtmlAgilityPack;
using System.Xml.XPath;

namespace RobloxAPI
{

    #region Enumerations
    
        public enum RobloxMembershipType
        {
            /// <summary>
            /// Not a BC member
            /// </summary>
            None = 0,
            /// <summary>
            /// Builder's Club or BC
            /// </summary>
            BuildersClub = 1,
            /// <summary>
            /// Turbo Builders Club or TBC
            /// </summary>
            TurboBuildersClub = 2,
            /// <summary>
            /// Outrageous Builders Club or OBC
            /// </summary>
            OutrageousBuildersClub = 3
        }

        public enum RobloxAssetType
        {
            TShirt = 2,
            Audio = 3,
            Hat = 8,
            Model = 10,
            Shirt = 11,
            Pants = 12,
            Decal = 13,
            Head = 17,
            Face = 18,
            Gear = 19,
            Animation = 24,
            Package = 32,
            Plugin = 38
        }
    #endregion

    #region Extension Methods

        public static class ExtensionMethods
        {

            public static HtmlNode GetElementByClass(this HtmlNode node, string className, string eleName)
            {
                return node.Descendants(eleName).Where(d =>
                    d.Attributes.Contains("class") && d.Attributes["class"].Value.Split(' ').Any(b => b.Equals(className))
                ).ToArray()[0];
            }

            public static HtmlNode GetElementByClass(this HtmlDocument node, string className, string eleName)
            {
                return node.DocumentNode.Descendants(eleName).Where(d =>
                    d.Attributes.Contains("class") && d.Attributes["class"].Value.Split(' ').Any(b => b.Equals(className))
                ).ToArray()[0];
            }

            public static HtmlNode[] GetElementsByClass(this HtmlDocument node, string className, string eleName)
            {
                return node.DocumentNode.Descendants(eleName).Where(d =>
                    d.Attributes.Contains("class") && d.Attributes["class"].Value.Split(' ').Any(b => b.Equals(className))
                ).ToArray();
            }

            public static void LoadFromUri(this HtmlDocument doc, string url)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(new WebClient().OpenRead(url)))
                    {
                        doc.LoadHtml(reader.ReadToEnd());
                    }
                }
                catch { }
            }
        }

    #endregion

    /// <summary>
    /// Credit: Roblox for making it and ZMidnight for making part of the custom ROBLOX web api.
    /// </summary>
    public static class RobloxApi
    {

        private static string ROBLOXAPI_SITE = "http://api.roblox.com/";

        #region API Methods

        /// <summary>
        /// Checks if Roblox's Database is running, usefull for the ROBLOX API.
        /// </summary>
        /// <returns>Is roblox's database running.</returns>
        public static bool IsRobloxDatabaseRunning()
        {
            WebClient c = new WebClient();
            using (StreamReader reader = new StreamReader(c.OpenRead("http://www.roblox.com/HealthMonitor/DBServerDiagnostics.ashx")))
            {
                if (reader.ReadToEnd() == "OK")
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns the friends based on the user id.
        /// </summary>
        /// <param name="userId">The user id to look up friends</param>
        /// <returns>The friends based on the user.</returns>
        public static User[] GetUserFriends(int userId)
        {
            try
            {
                using (StreamReader reader = new StreamReader(new WebClient().OpenRead(ROBLOXAPI_SITE + "users/" + userId + "/friends")))
                {
                    return JsonConvert.DeserializeObject<User[]>(reader.ReadToEnd());
                }
            }
            catch 
            {
                return new User[1]; 
            }
        }

        /// <summary>
        /// Gets the Asset Info by id
        /// </summary>
        /// <param name="prodId">The id of the asset.</param>
        /// <returns>The info of the item.</returns>
        public static ProductInfo GetAssetInfo(int prodId)
        {
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead(ROBLOXAPI_SITE+"marketplace/productinfo?assetId="+prodId)))
            {
                return JsonConvert.DeserializeObject<ProductInfo>(reader.ReadToEnd());
            }
        }

        /// <summary>
        /// Gets the Developer Product info by id
        /// </summary>
        /// <param name="prodId">The id of the product.</param>
        /// <returns>The info of the item.</returns>
        public static ProductInfo GetProductInfo(int prodId)
        {
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead(ROBLOXAPI_SITE + "marketplace/productdetails?productId=" + prodId)))
            {
                return JsonConvert.DeserializeObject<ProductInfo>(reader.ReadToEnd());
            }
        }

        /// <summary>
        /// Gets the places from an userId.
        /// </summary>
        /// <param name="userId">The user to get the places from</param>
        /// <returns>The places</returns>
        /// <seealso cref="Places"/>
        public static Places GetPlacesFrom(int userId)
        {
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead("http://www.roblox.com/Contests/Handlers/Showcases.ashx?userId="+userId)))
            {
                return JsonConvert.DeserializeObject<Places>(reader.ReadToEnd());
            }
        }

        /// <summary>
        /// Checks if a user is friends with another person.
        /// </summary>
        /// <param name="firstUserId">The first user to check friend status.</param>
        /// <param name="secondUserId">The second user to check friend status.</param>
        /// <returns>Are the two users friends?</returns>
        public static bool IsFriendsWith(int firstUserId, int secondUserId)
        {
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead("http://www.roblox.com/Game/LuaWebService/HandleSocialRequest.ashx?method=IsFriendsWith&playerId=" + firstUserId + "&userId=" + secondUserId)))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().TrimStart(' ');
                    if (line.ToLower().StartsWith("<value type=\"boolean\">") && line.ToLower().EndsWith("</value>"))
                    {
                        string rep = line.ToLower().Replace("<value type=\"boolean\">", "").Replace("</value>", "");
                        bool ret = false;
                        bool.TryParse(rep, out ret);
                        return ret;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if a user is a best friend with another person.
        /// </summary>
        /// <param name="firstUserId">The first user to check best friend status.</param>
        /// <param name="secondUserId">The second user to check best friend status.</param>
        /// <returns>Are the two users best friends?</returns>
        public static bool IsBestFriendsWith(int firstUserId, int secondUserId)
        {
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead("http://www.roblox.com/Game/LuaWebService/HandleSocialRequest.ashx?method=IsBestFriendsWith&playerId="+firstUserId+"&userId="+secondUserId)))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().TrimStart(' ');
                    if (line.ToLower().StartsWith("<value type=\"boolean\">") && line.ToLower().EndsWith("</value>"))
                    {
                        string rep = line.ToLower().Replace("<value type=\"boolean\">", "").Replace("</value>", "");
                        bool ret = false;
                        bool.TryParse(rep, out ret);
                        return ret;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Get's the username by id.
        /// </summary>
        /// <param name="uId">The id</param>
        /// <returns>The username from the id</returns>
        public static string GetUsernameFromId(int uId)
        {
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead(ROBLOXAPI_SITE+"users/"+uId)))
            {
                return JsonConvert.DeserializeObject<User>(reader.ReadToEnd()).Username;
            }
        }

        /// <summary>
        /// Get's the thumbnail image from a product info object.
        /// </summary>
        /// <param name="info">The object to get the image from.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <returns>The image from the object</returns>
        public static Image GetThumbnailImage(ProductInfo info, int width, int height)
        {
            WebClient c = new WebClient();
            Stream s = c.OpenRead("http://www.roblox.com/Thumbs/Asset.ashx?assetId=" + info.AssetId + "&width=" + width + "&height=" + height);
            return Image.FromStream(s);
        }

        /// <summary>
        /// Get's the new ROBLOX Leaderboard that was released on April 21, 2014 which will go away then come back.
        /// </summary>
        /// <returns>The leaderboard.</returns>
        public static Leaderboard GetLeaderboard()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadFromUri("http://web.roblox.com/leaderboards");
            HtmlNode[] nodes = doc.GetElementsByClass("leaderboards-row", "div");
            Leaderboard leader = new Leaderboard();
            leader.TopUsers = new List<User>();
            foreach (HtmlNode node in nodes)
            {
                try
                {
                    leader.TopUsers.Add(GetUserByUsername(node.GetElementByClass("roblox-name-column-link", "div").InnerText));
                }
                catch { }
            }
            return leader;
        }

        /// <summary>
        /// Get's the user's blurb by user id.
        /// </summary>
        /// <param name="uId">The user id</param>
        /// <returns>The user's blurb.</returns>
        public static string GetUserBlurb(int uId)
        {
            WebClient c = new WebClient();
            using(StreamReader reader = new StreamReader(c.OpenRead("http://rbxapi.zmidnight.net/user/blurb/?UserId="+uId)))
            {
                string f = reader.ReadToEnd();
                if (f.StartsWith("R/xhtml1/DTD/xhtml1-transitional.dtd\">")) // Prevents a long arse return which is just part of the html.
                {
                    return "";
                }
                return f;
            }
        }

        /// <summary>
        /// Get's the user's thumbnail with default size.
        /// </summary>
        /// <param name="usr">The user to get the thumbnail for.</param>
        /// <returns>The user's thumbnail.</returns>
        public static Image GetUserThumbnail(User usr)
        {
            WebClient c = new WebClient();
            Stream s = c.OpenRead("http://www.roblox.com/Thumbs/Avatar.ashx?userId=" + usr.Id);
            return Image.FromStream(s);
        }

        /// <summary>
        /// Get's the user's thumbnail with default size.
        /// </summary>
        /// <param name="usr">The user to get the thumbnail for.</param>
        /// <returns>The user's thumbnail.</returns>
        public static Image GetUserThumbnail(int uId)
        {
            WebClient c = new WebClient();
            Stream s = c.OpenRead("http://www.roblox.com/Thumbs/Avatar.ashx?userId=" + uId);
            return Image.FromStream(s);
        }

        /// <summary>
        /// Get's the user's thumbnail with default size.
        /// </summary>
        /// <param name="usr">The user to get the thumbnail for.</param>
        /// <returns>The user's thumbnail.</returns>
        public static Image GetUserThumbnail(int uId, int width, int height)
        {
            WebClient c = new WebClient();
            Stream s = c.OpenRead("http://www.roblox.com/Thumbs/Avatar.ashx?userId=" + uId+"&width="+width+"&height="+height);
            return Image.FromStream(s);
        }

        /// <summary>
        /// Get's the user's thumbnail.
        /// </summary>
        /// <param name="usr">The user to get the thumbnail for</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <returns>The user's thumbnail.</returns>
        public static Image GetUserThumbnail(User usr, int width, int height)
        {
            WebClient c = new WebClient();
            Stream s = c.OpenRead("http://www.roblox.com/Thumbs/Avatar.ashx?userId=" + usr.Id + "&width="+width+"&height="+height);
            return Image.FromStream(s);
        }

        /// <summary>
        /// Get a user object by username
        /// </summary>
        /// <param name="username">The username to get the object</param>
        /// <returns>The user object. Can be A blank user object with id of -1 if user doesn't exist or fails parsing.</returns>
        public static User GetUserByUsername(string username)
        {
            MyWebClient c = new MyWebClient();
            c.OpenRead("http://roblox.com/User.aspx?username="+username);
            string id = c.ResponseUri.PathAndQuery.Replace("/User.aspx?ID=", "");
            Console.WriteLine(id);
            int i = -1;
            int.TryParse(id, out i);
            if (i > 0)
            {
                return GetUserById(i);
            }
            else
            {
                return new User()
                {
                    Id = -1,
                    Username = ""
                };
            }
        }

        /// <summary>
        /// Searches Roblox with a query.
        /// </summary>
        /// <param name="q">The query</param>
        /// <returns>Places searched using the query</returns>
        public static Place[] SearchForPlaces(SearchQuery q)
        {
            string url = "http://web.roblox.com/games/list-json?"+q.ToString();
            WebClient client = new WebClient();
            using (StreamReader reader = new StreamReader(client.OpenRead(url)))
            {
                return JsonConvert.DeserializeObject<Place[]>(reader.ReadToEnd());
            }
        }

        /// <summary>
        /// Gets an user object by id.
        /// </summary>
        /// <param name="uId">The id to get the user object.</param>
        /// <returns>The object of the user.</returns>
        public static User GetUserById(int uId)
        {
            try
            {
                WebClient client = new WebClient();
                using (StreamReader reader = new StreamReader(client.OpenRead(ROBLOXAPI_SITE + "users/" + uId)))
                {
                    return JsonConvert.DeserializeObject<User>(reader.ReadToEnd());
                }
            }
            catch
            {
                return new User()
                {
                    Id = -1
                };
            }
        }

        #endregion

    }
}
